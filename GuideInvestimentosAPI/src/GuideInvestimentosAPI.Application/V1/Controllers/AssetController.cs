using AutoMapper;
using GuideInvestimentosAPI.Application.Controllers;
using GuideInvestimentosAPI.Application.ViewModels;
using GuideInvestimentosAPI.Business.Interfaces;
using GuideInvestimentosAPI.Business.Interfaces.Notifications;
using GuideInvestimentosAPI.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using GuideInvestimentosAPI.Business.Notifications;


namespace GuideInvestimentosAPI.Application.V1.Controllers
{
    //Route("v{version:apiVersion}/Asset/[controller]")]
    [ApiVersion("1.0")]
    public class AssetController : MainController
    {
        private readonly IAssetService _assetService;
        private readonly IAssetRepository _assetRepository;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public AssetController(HttpClient httpClient, 
                                IAssetService assetService, 
                                INotificator notificator, 
                                IAssetRepository assetRepository, 
                                IMapper mapper) : base(notificator)
        {
            _httpClient = httpClient;
            _assetService = assetService;
            _assetRepository = assetRepository;
            _mapper = mapper;
            _notificator = notificator;

        }

        [HttpGet("{asset}")]
        public async Task<ActionResult<List<GetAssetViewModel>>> Get(string asset)
        {
            var resultAsset = await _assetRepository.GetAsset(asset + ".SA");

            if (!resultAsset.Any())
            {
                _notificator.AddNotification(new Notification($"404: The asset {asset} does not exist."));
                return ApiResponse();
            }

            var result = _mapper.Map<List<GetAssetViewModel>>(resultAsset).OrderBy(x => x.Date).ToList();

            return ApiResponse(CalculateVariantion(result));
        }

        [HttpPost("new-asset")]
        public async Task<ActionResult<AssetViewModel>> Register([FromBody] AssetViewModel assetViewModel)
        {
            if (!ModelState.IsValid)
            {
                return ApiResponse(ModelState);
            }

            try
            {
                var response = await _httpClient.GetStringAsync($"https://query2.finance.yahoo.com/v8/finance/chart/{assetViewModel.Symbol}.SA");
                var responseObject = JsonSerializer.Deserialize<RootObject>(response);

                if (responseObject.chart.result[0].indicators.quote[0].open == null || responseObject.chart.result[0].timestamp == null)
                {
                    _notificator.AddNotification(new Notification("Yahoo API with error."));
                    return ApiResponse();
                }

                var success = await _assetService.SaveAsset(responseObject);

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("404"))
                {
                    _notificator.AddNotification(new Notification($"404: The asset {assetViewModel.Symbol} does not exist."));
                }
                else
                {
                    _notificator.AddNotification(new Notification("Error, please contact our services."));
                }

                return ApiResponse();
            }

            return ApiResponse(assetViewModel);
        }

        private List<GetAssetViewModel> CalculateVariantion(List<GetAssetViewModel> assets)
        {
            var firstDateAsset = assets.First();

            for (int i = 1; i < assets.Count; i++)
            {
                assets[i].VariationFirstDate = (((assets[i].Value / firstDateAsset.Value) - 1) * 100).ToString("F") + "%";

                if (assets[i - 1].Value != 0)
                {
                    assets[i].VariationD1 = (((assets[i].Value / assets[i - 1].Value) - 1) * 100).ToString("F") + "%";
                }
                else
                {
                    assets[i].VariationD1 = string.Empty;
                }

            }

            return assets;
        }
    }
}

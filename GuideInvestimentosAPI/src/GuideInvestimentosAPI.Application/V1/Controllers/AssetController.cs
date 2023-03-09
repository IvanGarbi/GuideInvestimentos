using AutoMapper;
using GuideInvestimentosAPI.Application.Controllers;
using GuideInvestimentosAPI.Application.ViewModels;
using GuideInvestimentosAPI.Business.Interfaces;
using GuideInvestimentosAPI.Business.Interfaces.Notifications;
using GuideInvestimentosAPI.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
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

        }

        [HttpGet("{asset}")]
        public async Task<ActionResult<List<GetAssetViewModel>>> Get(string asset)
        {
            var resultAsset = await _assetRepository.GetAsset(asset + ".SA");

            if (!resultAsset.Any())
            {
                return NotFound();
            }
            var result = _mapper.Map<List<GetAssetViewModel>>(resultAsset).OrderBy(x => x.Date).ToList();

            var firstDateAsset = result.First();

            for (int i = 1; i < result.Count; i++)
            {
                result[i].VariationFirstDate = (((result[i].Value / firstDateAsset.Value) - 1) * 100).ToString("F") + "%";
                result[i].VariationD1 = (((result[i].Value / result[i -1].Value) - 1) * 100).ToString("F") + "%";
            }

            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] AssetViewModel assetViewModel)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            try
            {
                var response = await _httpClient.GetStringAsync($"https://query2.finance.yahoo.com/v8/finance/chart/{assetViewModel.Symbol}.SA");
                var responseObject = JsonSerializer.Deserialize<RootObject>(response);

                var success = await _assetService.SaveAsset(responseObject);

            }
            catch (Exception)
            {
                return NotFound();
                throw;
            }


            return Ok();
        }
    }
}

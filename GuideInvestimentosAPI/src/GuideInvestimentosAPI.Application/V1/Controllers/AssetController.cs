using GuideInvestimentosAPI.Application.Controllers;
using GuideInvestimentosAPI.Application.ViewModels;
using GuideInvestimentosAPI.Business.Interfaces;
using GuideInvestimentosAPI.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;


namespace GuideInvestimentosAPI.Application.V1.Controllers
{
    //Route("v{version:apiVersion}/Asset/[controller]")]
    [ApiVersion("1.0")]
    public class AssetController : MainController
    {
        private readonly IAssetService _assetService;
        private readonly HttpClient _httpClient;

        public AssetController(HttpClient httpClient, IAssetService assetService)
        {
            _httpClient = httpClient;
            _assetService = assetService;
        }

        [HttpGet("{asset}")]
        public async Task<ActionResult> Get(string asset)
        {
            return Ok();
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

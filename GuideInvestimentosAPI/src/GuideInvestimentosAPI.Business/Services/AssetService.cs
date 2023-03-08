using GuideInvestimentosAPI.Business.Interfaces;
using GuideInvestimentosAPI.Business.Models;

namespace GuideInvestimentosAPI.Business.Services
{
    public class AssetService : IAssetService
    {

        public async Task<bool> SaveAsset(RootObject responseObject)
        {
            List<Asset> assets = new List<Asset>();

            for (int i = 0; i < 30; i++)
            {
                Asset asset = new Asset();
                asset.Currency = responseObject.chart.result[0].meta.currency;
                asset.Symbol = responseObject.chart.result[0].meta.symbol;
                asset.Date = new DateTime(responseObject.chart.result[0].timestamp[i]);

                if (responseObject.chart.result[0].indicators.quote[0].open[0].HasValue)
                {
                    asset.Value = responseObject.chart.result[0].indicators.quote[0].open[0].Value;
                }
                else
                {
                    asset.Value = 0;
                }
            }


            return true;
        }
    }
}

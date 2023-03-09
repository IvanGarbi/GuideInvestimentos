using FluentValidation;
using FluentValidation.Results;
using GuideInvestimentosAPI.Business.Interfaces;
using GuideInvestimentosAPI.Business.Interfaces.Notifications;
using GuideInvestimentosAPI.Business.Models;
using GuideInvestimentosAPI.Business.Models.Validations;
using GuideInvestimentosAPI.Business.Notifications;

namespace GuideInvestimentosAPI.Business.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly INotificator _notificator;

        public AssetService(IAssetRepository assetRepository, INotificator notificator)
        {
            _assetRepository = assetRepository;
            _notificator = notificator;
        }

        public async Task<bool> SaveAsset(RootObject responseObject)
        {
            List<Asset> assets = new List<Asset>();

            var responseObjectOpenLength = responseObject.chart.result[0].indicators.quote[0].open.Length;

            if (responseObjectOpenLength > 30) 
            {
                responseObjectOpenLength = 30;
            }

            for (int i = 0; i < responseObjectOpenLength; i++)
            {
                Asset asset = new Asset();
                asset.Currency = responseObject.chart.result[0].meta.currency;
                asset.Symbol = responseObject.chart.result[0].meta.symbol;
                asset.Date = DateTimeOffset.FromUnixTimeSeconds(responseObject.chart.result[0].timestamp[i]).LocalDateTime;

                if (responseObject.chart.result[0].indicators.quote[0].open[i].HasValue)
                {
                    asset.Value = responseObject.chart.result[0].indicators.quote[0].open[i].Value;
                }
                else
                {
                    asset.Value = 0;
                }

                assets.Add(asset);
            }


            bool validation;
            foreach (var asset in assets)
            {
                validation = Validate(new AssetValidation(), asset);
                if (!validation)
                {
                    return validation;
                }
            }

            await _assetRepository.InsertAsset(assets);

            return true;
        }

        private bool Validate<TV, Asset>(TV validation, Asset entity) where TV : AbstractValidator<Asset>
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid)
            {
                return true;
            }

            foreach (var error in validator.Errors)
            {
                _notificator.AddNotification(new Notification(error.ErrorMessage));
            }

            return false;
        }

        public void Dispose()
        {
            _assetRepository?.Dispose();
        }
    }
}

namespace GuideInvestimentosAPI.Tests.UnitTests.Fixtures
{
    [CollectionDefinition(nameof(AssetCollection))]
    public class AssetCollection : ICollectionFixture<AssetTestsFixture>
    { }
    public class AssetTestsFixture : IDisposable
    {
        public Business.Models.Asset CreateValidAsset()
        {
            var asset = new Business.Models.Asset();
            asset.Currency = "BRL";
            asset.Symbol = "WEGE3";
            asset.Date = DateTime.Now.AddMinutes(1);
            asset.Value = new decimal(new Random().NextDouble());

            return asset;
        }

        public Business.Models.Asset CreateInvalidAsset()
        {
            var asset = new Business.Models.Asset();
            asset.Currency = "";
            asset.Symbol = "";
            asset.Date = DateTime.Now.AddMinutes(1);
            asset.Value = new decimal(new Random().NextDouble());

            return asset;
        }

        public void Dispose()
        {

        }
    }
}

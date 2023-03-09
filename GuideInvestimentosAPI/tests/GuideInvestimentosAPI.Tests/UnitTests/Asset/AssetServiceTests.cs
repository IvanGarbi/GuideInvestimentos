using GuideInvestimentosAPI.Business.Models;
using GuideInvestimentosAPI.Business.Notifications;
using GuideInvestimentosAPI.Business.Services;
using GuideInvestimentosAPI.Data.Context;
using GuideInvestimentosAPI.Data.Repository;
using GuideInvestimentosAPI.Tests.UnitTests.Fixtures;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GuideInvestimentosAPI.Tests.UnitTests.Asset
{
    [Collection(nameof(AssetCollection))]
    public class AssetServiceTests
    {
        private readonly AssetTestsFixture _assetTestsFixture;

        public AssetServiceTests(AssetTestsFixture assetTestsFixture)
        {
            _assetTestsFixture = assetTestsFixture;
        }

        [Fact(DisplayName = "Add Asset With Success")]
        [Trait("Category", "Asset Service Tests")]
        public async void AssetService_Add_ExecuteWithSuccess()
        {
            //Arrange
            var root = new RootObject();
            var chart = new Chart();
            root.chart = chart;

            var meta = new Meta();
            var indicators = new Indicators();

            List<Quote> quoteList = new List<Quote>();
            List<decimal?> decimalList = new List<decimal?>();
            List<long> longList = new List<long>();
            quoteList.Add(new Quote());

            for (int i = 0; i < 5; i++)
            {
                var asset = _assetTestsFixture.CreateValidAsset();

                chart.result = new Result[] {
                    new Result()
                };

                chart.result[0].indicators = new Indicators();
                chart.result[0].meta = new Meta();

                longList.Add(LongRandom(1678366980, 1678566980, new Random()));

                chart.result[0].indicators.quote = quoteList.ToArray();

                decimalList.Add(asset.Value);

                root.chart.result[0].indicators.quote[0].open = decimalList.ToArray();       
                root.chart.result[0].meta.symbol = asset.Symbol;
                root.chart.result[0].meta.currency = asset.Currency;
                root.chart.result[0].timestamp = longList.ToArray(); 

            }

            var options = new DbContextOptionsBuilder<GuideInvestimentosApiDbContext>()
                .UseInMemoryDatabase("GuideInvestimentosApiDb1")
                .Options;

            var notification = new Mock<Notificator>();

            AssetRepository assetRepository = new AssetRepository(new GuideInvestimentosApiDbContext(options));

            // Act
            using (var context = new AssetService(new AssetRepository(new GuideInvestimentosApiDbContext(options)), notification.Object))
            {
                await context.SaveAsset(root);
            }

            var results = await assetRepository.GetAsset(root.chart.result[0].meta.symbol);
            var notificationList = notification.Object.ReturnNotifications();
            var emptyNotifications = notification.Object.EmptyNotifications();

            //Assert  
            Assert.NotNull(results);
            Assert.Equal(5, results.Count<Business.Models.Asset>());
            Assert.Empty(notificationList);
            Assert.False(emptyNotifications);
        }

        [Fact(DisplayName = "Add Asset Without Success")]
        [Trait("Category", "Asset Service Tests")]
        public async void AssetService_Add_ExecuteWithoutSuccess()
        {
            //Arrange
            var root = new RootObject();
            var chart = new Chart();
            root.chart = chart;

            var meta = new Meta();
            var indicators = new Indicators();

            List<Quote> quoteList = new List<Quote>();
            List<decimal?> decimalList = new List<decimal?>();
            List<long> longList = new List<long>();
            quoteList.Add(new Quote());

            for (int i = 0; i < 5; i++)
            {
                var asset = _assetTestsFixture.CreateInvalidAsset();

                chart.result = new Result[] {
                    new Result()
                };

                chart.result[0].indicators = new Indicators();
                chart.result[0].meta = new Meta();

                longList.Add(LongRandom(1678366980, 1678566980, new Random()));

                chart.result[0].indicators.quote = quoteList.ToArray();

                decimalList.Add(asset.Value);

                root.chart.result[0].indicators.quote[0].open = decimalList.ToArray();
                root.chart.result[0].meta.symbol = asset.Symbol;
                root.chart.result[0].meta.currency = asset.Currency;
                root.chart.result[0].timestamp = longList.ToArray();

            }


            var options = new DbContextOptionsBuilder<GuideInvestimentosApiDbContext>()
                .UseInMemoryDatabase("GuideInvestimentosApiDb2")
                .Options;

            var notification = new Mock<Notificator>();


            AssetRepository assetRepository = new AssetRepository(new GuideInvestimentosApiDbContext(options));

            // Act
            using (var context = new AssetService(new AssetRepository(new GuideInvestimentosApiDbContext(options)), notification.Object))
            {
                await context.SaveAsset(root);
            }

            var results = await assetRepository.GetAsset(root.chart.result[0].meta.symbol);
            var notificationList = notification.Object.ReturnNotifications();
            var emptyNotifications = notification.Object.EmptyNotifications();

            //Assert  
            Assert.Empty(results);
            Assert.Equal(0, results.Count<Business.Models.Asset>());
            Assert.NotEmpty(notificationList);
            Assert.Equal(2, notificationList.Count);
            Assert.True(emptyNotifications);
        }

        long LongRandom(long min, long max, Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }
    }
}

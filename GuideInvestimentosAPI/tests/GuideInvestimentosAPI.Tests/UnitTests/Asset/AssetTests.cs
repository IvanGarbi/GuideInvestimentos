using GuideInvestimentosAPI.Business.Models.Validations;
using GuideInvestimentosAPI.Tests.UnitTests.Fixtures;

namespace GuideInvestimentosAPI.Tests.UnitTests.Asset
{
    [Collection(nameof(AssetCollection))]
    public class AssetTests
    {
        private readonly AssetTestsFixture _assetTestsFixture;

        public AssetTests(AssetTestsFixture assetTestsFixture)
        {
            _assetTestsFixture = assetTestsFixture;
        }

        [Fact(DisplayName = "New Valid Asset")]
        [Trait("Category", "Asset Fixture Tests")]
        public void Asset_NewAsset_MustBeValid()
        {
            // Arrange
            var asset = _assetTestsFixture.CreateValidAsset();

            // Act
            var result = new AssetValidation().Validate(asset);

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact(DisplayName = "New Invalid Asset")]
        [Trait("Category", "Asset Fixture Tests")]
        public void Asset_NewAsset_MustBeInvalid()
        {
            // Arrange
            var asset = _assetTestsFixture.CreateInvalidAsset();

            // Act
            var result = new AssetValidation().Validate(asset);

            // Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(2, result.Errors.Count);
        }
    }
}

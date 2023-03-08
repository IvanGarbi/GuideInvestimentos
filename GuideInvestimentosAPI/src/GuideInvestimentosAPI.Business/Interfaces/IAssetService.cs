using GuideInvestimentosAPI.Business.Models;

namespace GuideInvestimentosAPI.Business.Interfaces
{
    public interface IAssetService
    {
        Task<bool> SaveAsset(RootObject responseObject);
    }
}

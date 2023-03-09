using GuideInvestimentosAPI.Business.Models;

namespace GuideInvestimentosAPI.Business.Interfaces
{
    public interface IAssetService : IDisposable
    {
        Task<bool> SaveAsset(RootObject responseObject);
    }
}

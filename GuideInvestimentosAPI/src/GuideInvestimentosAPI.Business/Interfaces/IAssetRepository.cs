
using GuideInvestimentosAPI.Business.Models;

namespace GuideInvestimentosAPI.Business.Interfaces
{
    public interface IAssetRepository : IDisposable
    {
        Task InsertAsset(List<Asset> assets);
        Task<IEnumerable<Asset>> GetAsset(string symbol);
        Task<int> SaveChanges();
    }
}

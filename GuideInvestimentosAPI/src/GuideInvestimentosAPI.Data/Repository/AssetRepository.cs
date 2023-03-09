
using GuideInvestimentosAPI.Business.Interfaces;
using GuideInvestimentosAPI.Business.Models;
using GuideInvestimentosAPI.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GuideInvestimentosAPI.Data.Repository
{
    public class AssetRepository : IAssetRepository
    {
        protected readonly GuideInvestimentosApiDbContext Db;
        protected readonly DbSet<Asset> DbSet;

        public AssetRepository(GuideInvestimentosApiDbContext db)
        {
            Db = db;
            DbSet = db.Set<Asset>();
        }

        public async Task InsertAsset(List<Asset> assets)
        {
            Db.AddRange(assets);
            await SaveChanges();
        }

        public async Task<IEnumerable<Asset>> GetAsset(string symbol)
        {
            return await DbSet.AsNoTracking().Where(x => x.Symbol == symbol).ToListAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public async void Dispose()
        {
            Db?.Dispose();
        }
    }
}

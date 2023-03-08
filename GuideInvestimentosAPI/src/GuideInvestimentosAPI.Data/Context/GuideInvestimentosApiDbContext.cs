using GuideInvestimentosAPI.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace GuideInvestimentosAPI.Data.Context
{
    public class GuideInvestimentosApiDbContext : DbContext
    {
        public GuideInvestimentosApiDbContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Asset> Assets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                         .SelectMany(e => e.GetProperties()
                             .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(20)");

            foreach (var property in modelBuilder.Model.GetEntityTypes()
             .SelectMany(e => e.GetProperties()
                 .Where(p => p.ClrType == typeof(decimal))))
                property.SetColumnType("decimal");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GuideInvestimentosApiDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}

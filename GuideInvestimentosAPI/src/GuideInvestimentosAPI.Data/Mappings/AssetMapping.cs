using GuideInvestimentosAPI.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuideInvestimentosAPI.Data.Mappings
{
    public class AssetMapping : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Date)
                .IsRequired()
                .HasColumnType("DATETIME");

            builder.Property(x => x.Symbol)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(10);

            builder.Property(x => x.Currency)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(5);

            builder.Property(x => x.Value)
                .IsRequired()
                .HasColumnType("DECIMAL");

            builder.ToTable("Asset");
        }
    }
}

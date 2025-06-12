using FlowaStudy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowaStudy.ORM.Configuration
{
    public class FinancialAssetConfiguration : IEntityTypeConfiguration<FinancialAsset>
    {
        public void Configure(EntityTypeBuilder<FinancialAsset> builder)
        {
            builder.ToTable("FinancialAssets");
            builder.HasKey(fa => fa.Id);
            builder.Property(fa => fa.Name).IsRequired().HasMaxLength(100);
            builder.Property(fa => fa.Value).IsRequired();
            builder.Property(fa => fa.AcquisitionDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}

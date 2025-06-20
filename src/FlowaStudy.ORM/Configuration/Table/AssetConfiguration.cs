using FlowaStudy.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowaStudy.ORM.Configuration.Table
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.ToTable("Assets");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                   .IsRequired();

            builder.Property(a => a.Symbol)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(a => a.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(a => a.CurrentPrice)
                   .HasColumnType("decimal(18,2)");

            builder.Property(a => a.LastUpdated)
                   .IsRequired();
        }
    }
}

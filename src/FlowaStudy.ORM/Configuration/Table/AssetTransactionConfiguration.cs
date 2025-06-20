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
    public class AssetTransactionConfiguration : IEntityTypeConfiguration<AssetTransaction>
    {
        public void Configure(EntityTypeBuilder<AssetTransaction> builder)
        {
            builder.ToTable("AssetTransactions");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                   .IsRequired();

            builder.Property(t => t.UserId)
                   .IsRequired();

            builder.Property(t => t.AssetId)
                   .IsRequired();

            builder.Property(t => t.Type)
                   .IsRequired();

            builder.Property(t => t.Quantity)
                   .HasColumnType("decimal(18,4)")
                   .IsRequired();

            builder.Property(t => t.PriceAtExecution)
                   .HasColumnType("decimal(18,4)")
                   .IsRequired();

            builder.Property(t => t.Timestamp)
                   .IsRequired();

        }
    }
}

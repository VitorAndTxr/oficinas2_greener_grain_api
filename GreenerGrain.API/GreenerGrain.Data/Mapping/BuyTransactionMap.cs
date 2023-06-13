using GreenerGrain.Framework.Database.EfCore.Mapping;
using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GreenerGrain.Data.Mapping
{
    public class BuyTransactionMap : BaseAuditEntityMap<BuyTransaction, Guid>
    {
        protected override void CreateModel(EntityTypeBuilder<BuyTransaction> builder)
        {
            builder.ToTable("BuyTransaction");

            builder
                 .Property(b => b.Quantity)
                 .IsRequired();
            builder
                 .Property(b => b.Value)
                 .IsRequired();

            builder
                 .Property(b => b.BuyerId)
                 .IsRequired();
            builder
                 .Property(b => b.ModuleId)
                 .IsRequired();
            builder
                 .Property(b => b.GrainId)
                 .IsRequired();

            builder
                .HasOne(x => x.Buyer)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Module)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Grain)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

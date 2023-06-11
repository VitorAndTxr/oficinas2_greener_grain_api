using GreenerGrain.Framework.Database.EfCore.Mapping;
using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Microsoft.EntityFrameworkCore;

namespace GreenerGrain.Data.Mapping
{
    public class UnitMap : BaseAuditEntityMap<Unit, Guid>
    {
        protected override void CreateModel(EntityTypeBuilder<Unit> builder)
        {
            builder
             .Property(b => b.Code)
             .IsRequired();

            builder
             .Property(b => b.Ip)
             .IsRequired();


            builder
             .Property(b => b.MAC)
             .IsRequired();

            builder
             .Property(b => b.State)
             .IsRequired();

            builder
             .Property(b => b.OwnerId)
             .IsRequired();

            builder
                .HasMany(x => x.Modules)
                .WithOne(x => x.Unit)
                .HasForeignKey(x => x.UnitId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

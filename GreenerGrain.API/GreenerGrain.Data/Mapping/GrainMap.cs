using GreenerGrain.Framework.Database.EfCore.Mapping;
using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GreenerGrain.Data.Mapping
{
    public class GrainMap : BaseAuditEntityMap<Grain, Guid>
    {
        protected override void CreateModel(EntityTypeBuilder<Grain> builder)
        {
            builder.ToTable("Grain");

            builder
                 .Property(b => b.Name)
                 .HasColumnType("varchar(150)")
                 .IsRequired();

            builder
                 .Property(b => b.Price)
                 .IsRequired();

            builder
                 .Property(b => b.ImageUrl)
                 .IsRequired();

            builder
             .Property(b => b.CreatorId)
             .IsRequired();

            builder
                .HasOne(x => x.Creator);

        }
    }
}

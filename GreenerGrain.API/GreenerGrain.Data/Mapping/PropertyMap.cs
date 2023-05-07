using GreenerGrain.Framework.Database.EfCore.Mapping;
using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GreenerGrain.Data.Mapping
{
    public class PropertyMap : BaseAuditEntityMap<Property, Guid>
    {
        protected override void CreateModel(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable("Property");

            builder
                 .Property(b => b.Name)
                 .HasColumnType("varchar(255)")
                 .HasMaxLength(255)
                 .IsRequired();

            builder
                .Property(b => b.CreationDate)
                .HasColumnType("timestamp")
                .IsRequired();

            builder
                .Property(b => b.UpdateDate)
                .HasColumnType("timestamp");

            builder
                .Property(b => b.DeleteDate)
                .HasColumnType("timestamp");

            builder.HasMany(x => x.InstitutionProviderProperties);
        }
    }

}

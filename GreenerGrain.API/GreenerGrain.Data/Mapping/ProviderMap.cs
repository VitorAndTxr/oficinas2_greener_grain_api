using GreenerGrain.Framework.Database.EfCore.Mapping;
using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GreenerGrain.Data.Mapping
{

    public class ProviderMap : BaseAuditEntityMap<Provider, Guid>
    {
        protected override void CreateModel(EntityTypeBuilder<Provider> builder)
        {
            builder.ToTable("Provider");

            builder
                 .Property(b => b.Name)
                 .HasColumnType("varchar(255)")
                 .HasMaxLength(255)
                 .IsRequired();

            builder
                .Property(b => b.Code)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();

            builder
                .Property(b => b.Description)
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

            builder.HasMany(x => x.InstitutionProviders);            
        }
    }

}

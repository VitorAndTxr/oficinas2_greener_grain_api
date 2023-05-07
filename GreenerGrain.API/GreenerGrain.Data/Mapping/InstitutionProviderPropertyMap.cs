using GreenerGrain.Framework.Database.EfCore.Mapping;
using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GreenerGrain.Data.Mapping
{
    public class InstitutionProviderPropertyMap : BaseAuditEntityMap<InstitutionProviderProperty, Guid>
    {
        protected override void CreateModel(EntityTypeBuilder<InstitutionProviderProperty> builder)
        {
            builder.ToTable("InstitutionProviderProperty");

            builder
                 .Property(b => b.InstitutionProviderId)
                 .IsRequired();

            builder
                 .Property(b => b.PropertyId)
                 .IsRequired();

            builder
                 .Property(b => b.Value)
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

            builder.HasOne(x => x.InstitutionProvider);
            builder.HasOne(x => x.Property);
        }
    }

}

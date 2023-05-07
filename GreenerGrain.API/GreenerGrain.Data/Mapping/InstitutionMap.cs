using GreenerGrain.Framework.Database.EfCore.Mapping;
using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GreenerGrain.Data.Mapping
{
    public class InstitutionMap : BaseAuditEntityMap<Institution, Guid>
    {
        protected override void CreateModel(EntityTypeBuilder<Institution> builder)
        {
            builder.ToTable("Institution");

            builder
                 .Property(b => b.Name)
                 .HasColumnType("varchar(200)")
                 .IsRequired();

            builder
               .Property(b => b.ProviderId)
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
            
            builder.HasOne(x => x.Provider);
        }
    }
}

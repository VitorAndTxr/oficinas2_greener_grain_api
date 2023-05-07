using GreenerGrain.Framework.Database.EfCore.Mapping;
using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GreenerGrain.Data.Mapping
{
    public class InstitutionServiceLocationMap : BaseAuditEntityMap<InstitutionServiceLocation, Guid>
    {
        protected override void CreateModel(EntityTypeBuilder<InstitutionServiceLocation> builder)
        {
            builder.ToTable("InstitutionServiceLocation");

            builder
               .Property(b => b.Description)
               .HasColumnType("varchar(100)")
               .HasMaxLength(100)
               .IsRequired();

            builder
               .Property(b => b.InstitutionId)
               .IsRequired();

            builder
               .Property(b => b.Remote)
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

            builder.HasMany(x => x.OfficerHours);
            builder.HasMany(x => x.AppointmentSchedules);
        }
    }
}

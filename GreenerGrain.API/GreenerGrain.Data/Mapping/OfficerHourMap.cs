using GreenerGrain.Framework.Database.EfCore.Mapping;
using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GreenerGrain.Data.Mapping
{
    public class OfficerHourMap : BaseAuditEntityMap<OfficerHour, Guid>
    {
        protected override void CreateModel(EntityTypeBuilder<OfficerHour> builder)
        {
            builder.ToTable("OfficerHour");

            builder
                 .Property(b => b.ServiceDeskOfficerId)
                 .IsRequired();

            builder
                 .Property(b => b.InstitutionServiceLocationId)
                 .IsRequired();            

            builder
                 .Property(b => b.DayOfWeek)
                 .HasColumnType("integer")
                 .IsRequired();

            builder
                .Property(b => b.StartTime)
                .HasConversion<long>()
                .IsRequired();

            builder
                .Property(b => b.EndTime)
                .HasConversion<long>()
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

            builder.HasOne(x => x.ServiceDeskOfficer);
            builder.HasOne(x => x.InstitutionServiceLocation);
        }
    }
}

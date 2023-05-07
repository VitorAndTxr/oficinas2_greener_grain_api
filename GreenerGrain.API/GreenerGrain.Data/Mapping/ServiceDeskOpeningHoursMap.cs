using GreenerGrain.Framework.Database.EfCore.Mapping;
using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GreenerGrain.Data.Mapping
{
    public class ServiceDeskOpeningHoursMap : BaseAuditEntityMap<ServiceDeskOpeningHours, Guid>
    {
        protected override void CreateModel(EntityTypeBuilder<ServiceDeskOpeningHours> builder)
        {
            builder.ToTable("ServiceDeskOpeningHours");

            builder
                 .Property(b => b.ServiceDeskId)
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

        }
    }
}

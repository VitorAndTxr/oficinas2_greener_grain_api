using GreenerGrain.Framework.Database.EfCore.Mapping;
using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GreenerGrain.Data.Mapping
{
    public class AppointmentScheduleMap : BaseAuditEntityMap<AppointmentSchedule, Guid>
    {
        protected override void CreateModel(EntityTypeBuilder<AppointmentSchedule> builder)
        {
            builder.ToTable("AppointmentSchedule");

            builder
                 .Property(b => b.InstitutionServiceLocationId)
                 .IsRequired();

            builder
                 .Property(b => b.AppointmentId);

            builder
                 .Property(b => b.ScheduledDate)
                 .HasColumnType("timestamp")
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

            builder.HasOne(x => x.Appointment);
            builder.HasOne(x => x.InstitutionServiceLocation);
        }
    }
}

using GreenerGrain.Framework.Database.EfCore.Mapping;
using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GreenerGrain.Data.Mapping
{
    public class AppointmentQueueMap : BaseAuditEntityMap<AppointmentQueue, Guid>
    {
        protected override void CreateModel(EntityTypeBuilder<AppointmentQueue> builder)
        {
            builder.ToTable("AppointmentQueue");            

            builder
                 .Property(b => b.AppointmentId);

            builder
                .Property(b => b.QueueEntranceHour)
                .HasColumnType("timestamp")
                .IsRequired();

            builder
                .Property(b => b.StartPosition)
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(b => b.QueueAttendHour)
                .HasColumnType("timestamp");

            builder
                .Property(b => b.AttendCloseHour)
                .HasColumnType("timestamp");



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
        }
    }
}

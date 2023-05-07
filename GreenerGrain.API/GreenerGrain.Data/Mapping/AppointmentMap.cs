using GreenerGrain.Framework.Database.EfCore.Mapping;
using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GreenerGrain.Data.Mapping
{
    public class AppointmentMap : BaseAuditEntityMap<Appointment, Guid>
    {
        protected override void CreateModel(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointment");

            builder
                 .Property(b => b.ServiceDeskId)
                 .IsRequired();

            builder
                 .Property(b => b.CustomerId);

            builder
                 .Property(b => b.OfficerId);

            builder
                 .Property(b => b.Date)
                 .HasColumnType("timestamp")
                 .IsRequired();

            builder
                 .Property(b => b.AppointmentStatusId)
                 .HasColumnType("integer")
                 .IsRequired();

            builder
                .Property(b => b.ProtocolNumber)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(b => b.MeetId)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder
                .Property(b => b.Note)
                .HasColumnType("varchar(500)")
                .HasMaxLength(500);

            builder
                .Property(b => b.CustomerRate)
                .HasColumnType("integer");

            builder
                .Property(b => b.CustomerNote)
                .HasColumnType("varchar(500)")
                .HasMaxLength(500);

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

            builder.HasOne(x => x.ServiceDesk);
            builder.HasOne(x => x.Customer);
            builder.HasOne(x => x.AppointmentSchedule);
            builder.HasOne(x => x.AppointmentQueue);
        }
    }
}

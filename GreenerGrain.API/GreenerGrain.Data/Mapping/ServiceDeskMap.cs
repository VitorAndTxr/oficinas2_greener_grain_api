using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenerGrain.Data.Mapping
{
    public class ServiceDeskMap : IEntityTypeConfiguration<ServiceDesk>
    {
        public void Configure(EntityTypeBuilder<ServiceDesk> builder)
        {
            builder.ToTable("ServiceDesk");

            builder
                .Property(b => b.ServiceDeskTypeId)
                .IsRequired();

            builder
                .Property(b => b.InstitutionId)
                .IsRequired();

            builder
                .Property(b => b.Description)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();

            builder
                .Property(b => b.Code)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(b => b.CalendarName)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(b => b.MeetDurationTime)
                .HasColumnType("integer")
                .IsRequired();

            builder
                .Property(b => b.CalendarTimeZone)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
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
            
            builder.HasOne(x => x.ServiceDeskType);
            builder.HasMany(x => x.ServiceDeskOfficers);
            builder.HasMany(x => x.Appointments);
            builder.HasMany(x => x.ServiceDeskOpeningHours);
        }
    }
}

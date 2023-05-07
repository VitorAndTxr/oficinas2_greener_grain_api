using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenerGrain.Data.Mapping
{
    public class ServiceDeskOfficerMap : IEntityTypeConfiguration<ServiceDeskOfficer>
    {
        public void Configure(EntityTypeBuilder<ServiceDeskOfficer> builder)
        {
            builder.ToTable("ServiceDeskOfficer");

            builder
                .Property(b => b.ServiceDeskId)
                .IsRequired();

            builder
                .Property(b => b.OfficerId)
                .IsRequired();

            builder
                .Property(b => b.Name)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsRequired();
            
            builder
                .Property(b => b.Email)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150)
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

            builder.HasOne(x => x.ServiceDesk);
            builder.HasMany(x => x.OfficerPauses);
            builder.HasMany(x => x.OfficerHours);

        }
    }
}

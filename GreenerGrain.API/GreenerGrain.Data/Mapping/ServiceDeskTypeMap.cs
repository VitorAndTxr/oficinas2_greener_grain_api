using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenerGrain.Data.Mapping
{
    public class ServiceDeskTypeMap : IEntityTypeConfiguration<ServiceDeskType>
    {
        public void Configure(EntityTypeBuilder<ServiceDeskType> builder)
        {
            builder.ToTable("ServiceDeskType");

            builder.HasKey(x => x.Id);

            builder
              .Property(b => b.Id)
              .ValueGeneratedNever();

            builder
                .Property(b => b.Description)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(x => x.ServiceDesks);
        }
    }
}

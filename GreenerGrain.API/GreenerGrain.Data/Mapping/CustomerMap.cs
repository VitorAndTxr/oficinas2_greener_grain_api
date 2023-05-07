using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenerGrain.Data.Mapping
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder
                .Property(b => b.Name)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(b => b.Email)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();

            builder
               .Property(b => b.PhoneNumber)
               .HasColumnType("varchar(50)")
               .HasMaxLength(50);

            builder
               .Property(b => b.Address)
               .HasColumnType("varchar(255)")
               .HasMaxLength(255);

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

            builder.HasMany(x => x.Appointments);            
        }
    }
}

using GreenerGrain.Framework.Database.EfCore.Mapping;
using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GreenerGrain.Data.Mapping
{
    public class OfficerPauseMap : BaseAuditEntityMap<OfficerPause, Guid>
    {
        protected override void CreateModel(EntityTypeBuilder<OfficerPause> builder)
        {
            builder.ToTable("OfficerPauseMap");

            builder
                 .Property(b => b.ServiceDeskOfficerId)
                 .IsRequired();

            builder
                 .Property(b => b.EntireDay)
                 .IsRequired();

            builder
                .Property(b => b.StartDate)
                .HasColumnType("timestamp")
                .IsRequired();

            builder
                .Property(b => b.EndDate)
                .HasColumnType("timestamp");

            builder
               .Property(b => b.Reason)
               .HasColumnType("varchar(255)")
               .HasMaxLength(255)
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
        }
    }
}

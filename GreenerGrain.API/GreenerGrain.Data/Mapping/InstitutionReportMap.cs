using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenerGrain.Data.Mapping
{
    public class InstitutionReportMap : IEntityTypeConfiguration<InstitutionReport>
    {
        public void Configure(EntityTypeBuilder<InstitutionReport> builder)
        {
            builder.ToTable("InstitutionReport");

            builder
                .Property(b => b.InstitutionId)
                .IsRequired();

            builder
                .Property(b => b.ReportTypeId)
                .IsRequired();                       

            builder
                .Property(b => b.ReportLink)
                .HasColumnType("TEXT")                
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
            
            builder.HasOne(x => x.ReportType);            
        }
    }
}

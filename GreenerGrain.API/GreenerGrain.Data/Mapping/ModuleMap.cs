using GreenerGrain.Framework.Database.EfCore.Mapping;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Module = GreenerGrain.Domain.Entities.Module;

namespace GreenerGrain.Data.Mapping
{
    public class ModuleMap : BaseAuditEntityMap<Module, Guid>
    {
        protected override void CreateModel(EntityTypeBuilder<Module> builder)
        {
            builder
             .Property(b => b.ContentLevel)
             .IsRequired();

            builder
             .Property(b => b.UnitId)
             .IsRequired();

            builder
             .Property(b => b.GrainId)
             .IsRequired();

            builder.HasOne(b => b.Unit);

            builder.HasOne(b => b.Grain);

        }
    }
}

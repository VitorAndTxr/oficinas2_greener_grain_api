using GreenerGrain.Framework.Database.EfCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenerGrain.Framework.Database.EfCore.Mapping
{
    public abstract class BaseAuditEntityMap<TEntity, TKey> : BaseActiveEntityMap<TEntity, TKey>
        where TEntity : AuditEntity<TKey>
    {

        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder
                .Property(b => b.CreationDate)
                .HasColumnType("DateTime")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder
                .Property(b => b.UpdateDate)
                .HasColumnType("DateTime");

            builder
                .Property(b => b.DeleteDate)
                .HasColumnType("DateTime");

            CreateModel(builder);
        }

    }
}

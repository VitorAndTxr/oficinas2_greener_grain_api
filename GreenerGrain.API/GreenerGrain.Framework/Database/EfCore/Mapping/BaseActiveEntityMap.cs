using GreenerGrain.Framework.Database.EfCore.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenerGrain.Framework.Database.EfCore.Mapping
{
    public abstract class BaseActiveEntityMap<TEntity, TKey> : BaseEntityMap<TEntity, TKey>
     where TEntity : ActiveEntity<TKey>
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder
                .Property(b => b.Active)
                .IsRequired();

            CreateModel(builder);
        }
    }
}

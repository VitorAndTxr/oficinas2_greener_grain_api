namespace GreenerGrain.Framework.Database.EfCore.Interface
{
    public interface IActiveEntity
    {
        bool Active { get; }
    }

    public interface IActiveEntity<TKey> : IActiveEntity, IBaseEntity<TKey>
    {
    }
}

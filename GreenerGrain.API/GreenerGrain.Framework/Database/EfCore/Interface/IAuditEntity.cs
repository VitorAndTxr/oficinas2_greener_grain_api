using System;

namespace GreenerGrain.Framework.Database.EfCore.Interface
{
    public interface IAuditEntity
    {
        public DateTime CreationDate { get; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }

    public interface IAuditEntity<TKey> : IAuditEntity, IActiveEntity<TKey>
    {
    }
}

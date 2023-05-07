using GreenerGrain.Framework.Database.EfCore.Model;
using System;

namespace GreenerGrain.Domain.Entities
{
    public class Institution : AuditEntity<Guid>
    {
        public Institution() { }

        public string Name { get; private set; }
        public virtual Provider Provider { get; set; }
        public Guid ProviderId { get; private set; }
    }
}

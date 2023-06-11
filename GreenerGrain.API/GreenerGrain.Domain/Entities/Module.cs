using GreenerGrain.Framework.Database.EfCore.Model;
using System;

namespace GreenerGrain.Domain.Entities
{
    public class Module : AuditEntity<Guid>
    {
        public Module()
        {
            SetId(Guid.NewGuid());
        }
        public float ContentLevel { get; private set; }
        public int Order { get; private set; }

        public Guid UnitId { get; private set; }
        public Guid GrainId { get; private set; }

        public virtual Unit Unit { get; private set; }
        public virtual Grain Grain { get; private set; }
    }
}
using GreenerGrain.Framework.Database.EfCore.Model;
using System;

namespace GreenerGrain.Domain.Entities
{
    public class BuyTransaction : AuditEntity<Guid>
    {
        public BuyTransaction()
        {
            SetId(Guid.NewGuid());
        }
        public float Quantity { get; private set; }
        public float Value { get; private set; }

        public Guid BuyerId { get; private set; }
        public Guid ModuleId { get; private set; }
        public Guid GrainId { get; private set; }

        public Account Buyer { get; private set; }
        public Module Module { get; private set; }
        public Grain Grain { get; private set; }
    }
}
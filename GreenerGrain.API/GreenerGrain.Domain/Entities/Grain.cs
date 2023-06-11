using GreenerGrain.Framework.Database.EfCore.Model;
using System;

namespace GreenerGrain.Domain.Entities
{
    public class Grain : AuditEntity<Guid>
    {
        public Grain()
        {
            SetId(Guid.NewGuid());
        }

        public string Name { get; private set; }
        public float Price { get; private set; }
        public string ImageUrl { get; private set; }
        public Guid CreatorId { get; private set; }
        public Account Creator { get; private set; }


    }
}
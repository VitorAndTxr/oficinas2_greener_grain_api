using GreenerGrain.Framework.Database.EfCore.Model;
using System;
using System.Collections.Generic;

namespace GreenerGrain.Domain.Entities
{
    public class Property : AuditEntity<Guid>
    {
        public string Name { get; set; }

        public virtual ICollection<InstitutionProviderProperty> InstitutionProviderProperties { get; private set; }
    }

}

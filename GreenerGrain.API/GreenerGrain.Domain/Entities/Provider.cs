using GreenerGrain.Framework.Database.EfCore.Model;
using System;
using System.Collections.Generic;

namespace GreenerGrain.Domain.Entities
{
    public class Provider : AuditEntity<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ICollection<InstitutionProvider> InstitutionProviders { get; private set; }
        public virtual ICollection<Account> Accounts { get; private set; }
    }

}

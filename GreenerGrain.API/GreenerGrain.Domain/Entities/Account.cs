using GreenerGrain.Framework.Database.EfCore.Model;
using System;

namespace GreenerGrain.Domain.Entities
{
    public class Account : AuditEntity<Guid>
    {
        protected Account() { }

        public Account(string login, string name, string password)
        {
            SetId(Guid.NewGuid());
            this.Login = login;
            this.Name = name;            
        }

        public string Login { get; private set; }
        public string Name { get; private set; }
        
        public Guid InstitutionId { get; private set; }
        public Guid ProviderId { get; private set; }

        public virtual Institution Institution { get; set; }
        public virtual Provider Provider { get; set; }

    }
}
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
        public string Password { get; private set; }



    }
}
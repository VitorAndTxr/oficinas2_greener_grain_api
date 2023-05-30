using GreenerGrain.Framework.Database.EfCore.Model;
using System;

namespace GreenerGrain.Domain.Entities
{
    public class Profile : AuditEntity<Guid>
    {
        protected Profile() { }

        public Profile(string name, string code)
        {
            SetId(Guid.NewGuid());
            this.Name = name;
            this.Code = code;
        }

        public string Name { get; private set; }
        public string Code { get; private set; }

    }

    public class ControlledUnit : AuditEntity<Guid>
    {
        protected ControlledUnit() { }

        public ControlledUnit(string name, string code)
        {
            SetId(Guid.NewGuid());
            this.Name = name;
            this.Code = code;
        }

        public string Name { get; private set; }
        public string Code { get; private set; }

    }
}

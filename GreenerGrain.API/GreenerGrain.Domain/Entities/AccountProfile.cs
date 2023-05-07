using GreenerGrain.Framework.Database.EfCore.Model;
using System;
using System.ComponentModel;

namespace GreenerGrain.Domain.Entities
{
    public class AccountProfile : AuditEntity<Guid>
    {   
        public Guid AccountId { get; set; }
        public Guid ProfileId { get; set; }

        [Description("ignore")]
        public virtual Account Account { get; set; }

        [Description("ignore")]
        public virtual Profile Profile { get; set; }
    }
}

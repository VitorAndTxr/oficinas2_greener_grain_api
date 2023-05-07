using GreenerGrain.Framework.Database.EfCore.Model;
using System;
using System.ComponentModel;

namespace GreenerGrain.Domain.Entities
{
    public class InstitutionProviderProperty : AuditEntity<Guid>
    {
        public Guid InstitutionProviderId { get; set; }
        public Guid PropertyId { get; set; }
        public string Value { get; set; }

        [Description("ignore")]
        public virtual Property Property { get; set; }

        [Description("ignore")]
        public virtual InstitutionProvider InstitutionProvider { get; set; }
    }

}

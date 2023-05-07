using GreenerGrain.Framework.Database.EfCore.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenerGrain.Domain.Entities
{
    public class InstitutionProvider : AuditEntity<Guid>
    {
        public Guid? InstitutionId { get; set; }
       
        public Guid? ProviderId { get; set; }

        [NotMapped]
        public string ProviderCode { get; set; }

        [Description("ignore")]
        public virtual Provider Provider { get; set; }

        public virtual ICollection<InstitutionProviderProperty> InstitutionProviderProperties { get; private set; }

    }
}

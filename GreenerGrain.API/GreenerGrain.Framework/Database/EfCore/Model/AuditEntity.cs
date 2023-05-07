using GreenerGrain.Framework.Database.EfCore.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenerGrain.Framework.Database.EfCore.Model
{
    public abstract class AuditEntity<TKey> : ActiveEntity<TKey>, IAuditEntity<TKey>
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; private set; } = DateTime.Now;

        public DateTime? UpdateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

    }
}

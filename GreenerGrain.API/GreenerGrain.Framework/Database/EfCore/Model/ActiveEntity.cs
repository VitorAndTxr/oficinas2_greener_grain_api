using GreenerGrain.Framework.Database.EfCore.Interface;
using System.ComponentModel.DataAnnotations;

namespace GreenerGrain.Framework.Database.EfCore.Model
{
    public abstract class ActiveEntity<TKey> : BaseEntity<TKey>, IActiveEntity<TKey>
    {
        [Required]
        public bool Active { get; private set; }

        /// <summary>
        /// Activate the entity
        /// </summary>
        public void Activate() => Active = true;

        /// <summary>
        /// Desactivate the entity
        /// </summary>
        public void Deactivate() => Active = false;
    }
}

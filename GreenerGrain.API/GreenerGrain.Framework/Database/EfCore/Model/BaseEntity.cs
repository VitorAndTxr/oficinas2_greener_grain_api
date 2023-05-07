using GreenerGrain.Framework.Database.EfCore.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenerGrain.Framework.Database.EfCore.Model
{
    public abstract class BaseEntity<TKey> : IBaseEntity<TKey>
    {
        public BaseEntity()
        {
            //Id = new Guid();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; private set; }

        /// <summary>
        /// Set ID Value based on TKey type
        /// </summary>
        /// <param name="id"></param>
        public void SetId(TKey id)
        {
            Id = id;
        }

        #region Comparators

        public override bool Equals(object obj)
        {
            var compareTo = obj as BaseEntity<TKey>;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(BaseEntity<TKey> a, BaseEntity<TKey> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(BaseEntity<TKey> a, BaseEntity<TKey> b)
        {
            return !(a == b);
        }

        #endregion

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
    }
}

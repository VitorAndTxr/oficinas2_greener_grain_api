using System.Collections.Generic;

namespace GreenerGrain.Framework.Sorting
{
    /// <summary>
    /// Contexto de sort (privado)
    /// </summary>
    public class SortingContext
    {
        /// <summary>
        /// Key and directions for sort 
        /// </summary>
        public List<SortKey> sortKeys { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        public SortingContext()
        {
            this.sortKeys = new List<SortKey>();
        }
    }
}

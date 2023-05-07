using System.Collections.Generic;

namespace GreenerGrain.Framework.Filtering
{
    /// <summary>
    /// 
    /// </summary>
    public class FilteringContext
    {
        /// <summary>
        /// Key and values for filter
        /// </summary>
        public List<FilterKey> filterKeys { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        public FilteringContext()
        {
            this.filterKeys = new List<FilterKey>();
        }
    }
}

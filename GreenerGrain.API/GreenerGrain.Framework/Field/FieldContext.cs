using System.Collections.Generic;

namespace GreenerGrain.Framework.Field
{
    /// <summary>
    /// Context for field selection
    /// </summary>
    public class FieldContext
    {
        /// <summary>
        /// Key and values for field selection
        /// </summary>
        public List<FieldKey> fieldKeys { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        public FieldContext()
        {
            this.fieldKeys = new List<FieldKey>();
        }
    }
}

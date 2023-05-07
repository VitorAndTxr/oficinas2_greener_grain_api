namespace GreenerGrain.Framework.Field
{
    /// <summary>
    /// 
    /// </summary>
    public class FieldKey
    {
        /// <summary>
        /// Chave para filtro
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// Valores do filtro
        /// </summary>
        public string[] values { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        public FieldKey(string key, string[] values)
        {
            this.key = key;
            this.values = values;
        }
    }
}

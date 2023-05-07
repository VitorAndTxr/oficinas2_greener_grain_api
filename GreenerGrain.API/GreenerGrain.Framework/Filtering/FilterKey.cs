namespace GreenerGrain.Framework.Filtering
{
    /// <summary>
    /// 
    /// </summary>
    public class FilterKey
    {
        /// <summary>
        /// Chave para filtro
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// Valores do filtro
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public FilterKey(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }
}

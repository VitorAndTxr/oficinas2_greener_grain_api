namespace GreenerGrain.Framework.Sorting
{
    /// <summary>
    /// Key and directions for sort 
    /// </summary>
    public class SortKey
    {
        /// <summary>
        /// Chave para ordenação
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// Direção da ordenação (ASC|DESC)
        /// </summary>
        public string direction { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="direction"></param>
        public SortKey(string key, string direction)
        {
            this.key = key;
            this.direction = direction;
        }
    }
}
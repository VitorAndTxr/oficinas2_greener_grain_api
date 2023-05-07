using System;

namespace GreenerGrain.Framework.Diagnostics
{
    /// <summary>
    /// Métrica de instrumentação do serviço.
    /// </summary>
    public class Metric
    {
        /// <summary>
        /// Nome da métrica.
        /// </summary>
        public string Name { get; } = "";

        /// <summary>
        /// DateTime de ocorrência dessa métrica.
        /// </summary>
        public DateTime DateTimeOccurrence { get; } = DateTime.Now;

        /// <summary>
        /// Valor da métrica.
        /// </summary>
        public float Value { get; } = 0;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="name">Nome da métrica.</param>
        /// <param name="value">Valor da métrica.</param>
        public Metric(string name, float value)
        {
            this.Name = name;
            this.Value = value;
            this.DateTimeOccurrence = DateTime.Now;
        }
    }
}

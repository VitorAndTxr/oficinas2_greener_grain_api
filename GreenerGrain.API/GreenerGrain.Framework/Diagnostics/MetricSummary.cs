using System.Collections.Generic;

namespace GreenerGrain.Framework.Diagnostics
{
    /// <summary>
    /// Resumo das métricas coletadas
    /// </summary>
    public class MetricSummary
    {
        /// <summary>
        /// Todas as métricas (sem filtro).
        /// </summary>
        public List<MetricSummaryGroup> All { get; }

        /// <summary>
        /// Métricas coletadas no último minuto.
        /// </summary>
        public List<MetricSummaryGroup> LastMinute { get; }

        /// <summary>
        /// Métricas coletadas nos últimos 5 minutos.
        /// </summary>
        public List<MetricSummaryGroup> Last5Minutes { get; }

        /// <summary>
        /// Métricas coletadas nos últimos 15 minutos.
        /// </summary>
        public List<MetricSummaryGroup> Last15Minutes { get; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="all">Todas as métricas (sem filtro).</param>
        /// <param name="lastMinute">Métricas coletadas no último minuto.</param>
        /// <param name="last5Minutes">Métricas coletadas nos últimos 5 minutos.</param>
        /// <param name="Last15Minutes">Métricas coletadas nos últimos 15 minutos.</param>
        public MetricSummary(List<MetricSummaryGroup> all,
                             List<MetricSummaryGroup> lastMinute,
                             List<MetricSummaryGroup> last5Minutes,
                             List<MetricSummaryGroup> Last15Minutes)
        {
            this.All = all;
            this.LastMinute = lastMinute;
            this.Last5Minutes = last5Minutes;
            this.Last15Minutes = Last15Minutes;
        }
    }
}

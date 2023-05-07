using System;

namespace GreenerGrain.Framework.Diagnostics
{
    /// <summary>
    /// Agrupador de métricos (filtro)
    /// </summary>
    public class MetricSummaryGroup
    {
        /// <summary>
        /// Nome do grupo ou filtro
        /// </summary>
        public string GroupName { get; } = "";

        /// <summary>
        /// Nome da métrica que foi agrupada/filtrada
        /// </summary>
        public string MetricName { get; } = "";

        /// <summary>
        /// Quantidade de ocorrências da métricas no grupo/filtro
        /// </summary>
        public long RecordCount { get; } = 0;

        /// <summary>
        /// Quantidade de registros por segundo da métrica no grupo/filtro
        /// </summary>
        public float RecordsPerSecond { get; } = 0;

        /// <summary>
        /// Valor mínimo registrado para a métrica
        /// </summary>
        public float? MinValue { get; } = null;

        /// <summary>
        /// Valor máximo registrado para a métrica
        /// </summary>
        public float? MaxValue { get; } = null;

        /// <summary>
        /// Valor médio registrado para a métrica
        /// </summary>
        public float? AvgValue { get; } = null;

        /// <summary>
        /// Mais recente ocorrência da métrica (dentro do grupo/filtro)
        /// </summary>
        public DateTime? LatestOccurrence { get; } = null;

        /// <summary>
        /// Mais antiga ocorrência da métrica (dentro do grupo/filtro)
        /// </summary>
        public DateTime? OldestOccurrence { get; } = null;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="groupName">Nome do grupo ou filtro</param>
        /// <param name="metricName">Nome da métrica que foi agrupada/filtrada</param>
        /// <param name="recordCount">Quantidade de ocorrências da métricas no grupo/filtro</param>
        /// <param name="recordsPerSecond">Quantidade de registros por segundo da métrica no grupo/filtro</param>
        /// <param name="minValue">Valor mínimo registrado para a métrica</param>
        /// <param name="maxValue">Valor máximo registrado para a métrica</param>
        /// <param name="avgValue">Valor médio registrado para a métrica</param>
        /// <param name="latestOccurrence">Mais recente ocorrência da métrica (dentro do grupo/filtro)</param>
        /// <param name="oldestOccurrence">Mais antiga ocorrência da métrica (dentro do grupo/filtro)</param>
        public MetricSummaryGroup(string groupName,
                                  string metricName,
                                  long recordCount,
                                  float recordsPerSecond,
                                  float? minValue,
                                  float? maxValue,
                                  float? avgValue,
                                  DateTime? latestOccurrence,
                                  DateTime? oldestOccurrence)
        {
            this.GroupName = groupName;
            this.MetricName = metricName;
            this.RecordCount = recordCount;
            this.RecordsPerSecond = recordsPerSecond;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.AvgValue = avgValue;
            this.LatestOccurrence = latestOccurrence;
            this.OldestOccurrence = oldestOccurrence;
        }
    }
}

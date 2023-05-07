using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenerGrain.Framework.Diagnostics
{
    /// <summary>
    /// Conjunto de métricas de instrumentação.
    /// </summary>
    public class Metrics
    {
        /// <summary>
        /// Referência interna estática para cache da lista de métricas
        /// </summary>
        private static object CachedList = null;

        /// <summary>
        /// Referencia interna para lista de métricas.
        /// </summary>
        private List<Metric> list = new List<Metric>();

        /// <summary>
        /// Adiciona uma nova métrica
        /// </summary>
        /// <param name="metricName">Nome da métrica</param>
        /// <param name="value">Valor da métrica.</param>
        public void Add(string metricName, float value)
        {
            RestoreFromCache();
            Purge();
            list.Add(new Metric(metricName, value));
            SaveToCache();
        }

        /// <summary>
        /// Retorna o resumo de todas as métricas coletadas.
        /// </summary>
        /// <returns>Resumo das métricas coletadas</returns>
        public MetricSummary GetSummary()
        {
            RestoreFromCache();
            List<string> availableMetrics = getAvailableMetrics();
            List<MetricSummaryGroup> all = filterMetrics(availableMetrics, 0);
            List<MetricSummaryGroup> lastMinute = filterMetrics(availableMetrics, 1);
            List<MetricSummaryGroup> last5Minutes = filterMetrics(availableMetrics, 5);
            List<MetricSummaryGroup> Last15Minutes = filterMetrics(availableMetrics, 15);

            MetricSummary metricSummary = new MetricSummary(all,
                                                            lastMinute,
                                                            last5Minutes,
                                                            Last15Minutes);
            return metricSummary;
        }

        /// <summary>
        /// Obtem a lista de métricas do Cache Estático.
        /// </summary>
        private void RestoreFromCache()
        {
            try
            {
                list = (List<Metric>)CachedList;
            }
            finally
            {
                if (list == null) list = new List<Metric>();
            }
        }

        /// <summary>
        /// Salva a lista de métricas no Cache Estático.
        /// </summary>
        private void SaveToCache()
        {
            CachedList = list;
        }

        /// <summary>
        /// Remove entradas antigas da lista de métricas para preservar memória.
        /// </summary>
        private void Purge()
        {
            //purge last 15 min;
            DateTime dateBase = DateTime.Now.AddMinutes(-15);
            list.RemoveAll(p => p.DateTimeOccurrence < dateBase);
        }

        /// <summary>
        /// Retorna uma lista simples somente com o nome das métricas adicionadas.
        /// </summary>
        /// <returns>Lista simples somente com o nome das métricas adicionadas.</returns>
        private List<string> getAvailableMetrics()
        {
            var groups = list.GroupBy(p => p.Name).ToList();
            List<string> result = new List<string>();
            foreach (var item in groups)
            {
                result.Add(item.Key);
            }

            return result;
        }

        /// <summary>
        /// Filtra as métricas por tempo.
        /// </summary>
        /// <param name="availableMetrics">Lista de nomes das métricas disponíveis.</param>
        /// <param name="minutes">Tempo a ser filtrada (em minutos)</param>
        /// <returns>Lista filtrada de métricas por tempo.</returns>
        private List<MetricSummaryGroup> filterMetrics(List<string> availableMetrics, int minutes)
        {
            List<MetricSummaryGroup> summaryGroupList = new List<MetricSummaryGroup>();
            DateTime filterEnd = DateTime.Now;
            DateTime filterStart = filterEnd.AddMinutes(-1 * minutes);
            string groupName = "";

            if (minutes == 0)
            {
                filterStart = filterEnd.AddYears(-10);
                groupName = "All";
            }
            else if (minutes == 1)
            {
                groupName = "Last 1 Minute";
            }
            else
            {
                groupName = "Last " + minutes + " Minutes";
            }

            foreach (var metricName in availableMetrics)
            {
                float recordsPerSecond = 0;
                float? minValue = null;
                float? maxValue = null;
                float? avgValue = null;
                DateTime? latestOccurrence = null;
                DateTime? oldestOccurrence = null;

                List<Metric> filter = list.Where(p => (p.DateTimeOccurrence >= filterStart) &&
                                                      (p.DateTimeOccurrence <= filterEnd) &&
                                                      (p.Name == metricName)).ToList();

                if (filter.Count > 0)
                {
                    oldestOccurrence = filter.Min(p => p.DateTimeOccurrence);
                    latestOccurrence = filter.Max(p => p.DateTimeOccurrence);
                    minValue = filter.Min(p => p.Value);
                    maxValue = filter.Max(p => p.Value);
                    avgValue = filter.Average(p => p.Value);

                    long totalSecondsInRange = (long)(latestOccurrence - oldestOccurrence).Value.TotalSeconds;
                    if (totalSecondsInRange == 0)
                    {
                        recordsPerSecond = (float)filter.Count;
                    }
                    else
                    {
                        recordsPerSecond = (float)filter.Count / (float)totalSecondsInRange;
                    }
                }

                MetricSummaryGroup metricSummaryGroup = new MetricSummaryGroup(groupName,
                                                                               metricName,
                                                                               filter.Count,
                                                                               recordsPerSecond,
                                                                               minValue,
                                                                               maxValue,
                                                                               avgValue,
                                                                               latestOccurrence,
                                                                               oldestOccurrence);

                summaryGroupList.Add(metricSummaryGroup);
            }

            return summaryGroupList;
        }
    }
}

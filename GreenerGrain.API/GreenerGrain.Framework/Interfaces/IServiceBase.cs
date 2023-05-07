using GreenerGrain.Framework.Diagnostics;

namespace GreenerGrain.Framework.Interfaces
{
    /// <summary>
    /// Interface base de implementação de serviços.
    /// </summary>
    public interface IServiceBase
    {
        /// <summary>
        /// Resumo das métricas coletadas para o serviço.
        /// </summary>
        /// <returns></returns>
        MetricSummary GetMetricSummary();

        /// <summary>
        /// Healtcheck do serviço.
        /// </summary>
        /// <returns>String indicando status de saúde do serviço. Uso livre para cada autor de cada serviço.</returns>
        string HealthCheck();
    }
}

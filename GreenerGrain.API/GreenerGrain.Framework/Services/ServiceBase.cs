using API.Framework.Interfaces;
using GreenerGrain.Framework.Diagnostics;
using GreenerGrain.Framework.Interfaces;
using System.Globalization;

namespace GreenerGrain.Framework.Services
{
    /// <summary>
    /// Classe base para implementação de serviços.
    /// </summary>
    public class ServiceBase : IServiceBase
    {
        private readonly IApiContext _apiContext;

        /// <summary>
        /// Nome do serviço.
        /// </summary>
        public virtual string ServiceName { get; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="apiContext"></param>
        public ServiceBase(IApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        /// <summary>
        /// Resumo das métricas coletadas para o serviço.
        /// </summary>
        /// <returns>Resumo das métricas coletadas para o serviço.</returns>
        public MetricSummary GetMetricSummary()
        {
            return _apiContext.InstrumentationContext.Metrics.GetSummary();
        }

        /// <summary>
        /// Healtcheck do serviço.
        /// </summary>
        /// <returns>String indicando status de saúde do serviço. Uso livre para cada autor de cada serviço.</returns>
        public virtual string HealthCheck()
        {
            return DateTime.Now.ToString(CultureInfo.CurrentCulture);
        }

    }
}

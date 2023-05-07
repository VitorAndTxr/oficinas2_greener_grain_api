using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GreenerGrain.Framework.Helpers.HttpClientHelpers
{
    /// <summary>
    /// Handler para tratar o timeout no HttpClient
    /// </summary>
    public class TimeoutHandler : DelegatingHandler
    {
        /// <summary>
        /// Timeout para a chamada
        /// </summary>
        public TimeSpan DefaultTimeout { get; set; } = TimeSpan.FromSeconds(120);

        private CancellationTokenSource GetCancellationTokenSource(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var timeout = request.GetTimeout() ?? DefaultTimeout;

            if (timeout == Timeout.InfiniteTimeSpan)
            {
                // Não há necessidade de criar um CTS se não houver tempo limite
                return null;
            }
            else
            {
                var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                cts.CancelAfter(timeout);

                return cts;
            }
        }

        /// <summary>
        /// Metodo que intercepta a chamada do HttpClient e faz o devido tratamento do Timeout da chamada
        /// </summary>
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (var cts = GetCancellationTokenSource(request, cancellationToken))
            {
                try
                {
                    return await base.SendAsync(request, cts?.Token ?? cancellationToken);
                }
                catch (OperationCanceledException)
                    when (!cancellationToken.IsCancellationRequested)
                {
                    throw new TimeoutException();
                }
            }
        }
    }
}

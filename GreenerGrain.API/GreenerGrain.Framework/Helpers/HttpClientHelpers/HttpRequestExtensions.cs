using System;
using System.Net.Http;

namespace GreenerGrain.Framework.Helpers.HttpClientHelpers
{
    /// <summary>
    /// Extensão do HttpClient para setar e tratar timeout de acesso
    /// </summary>
    public static class HttpRequestExtensions
    {
        private static readonly string _timeoutPropertyKey = "RequestTimeout";

        /// <summary>
        /// Seta o timeout para o HttpClient
        /// </summary>
        public static void SetTimeout(this HttpRequestMessage request, TimeSpan? timeout)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            request.Properties[_timeoutPropertyKey] = timeout;
        }

        /// <summary>
        /// Busca o timeout setado para o HttpClient
        /// </summary>
        public static TimeSpan? GetTimeout(this HttpRequestMessage request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Properties.TryGetValue(_timeoutPropertyKey, out var value) && value is TimeSpan timeout)
            {
                return timeout;
            }

            return null;
        }
    }
}

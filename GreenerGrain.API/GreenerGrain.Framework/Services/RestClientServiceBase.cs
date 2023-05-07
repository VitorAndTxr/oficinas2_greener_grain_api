using API.Framework.Interfaces;
using GreenerGrain.Framework.Exceptions;
using GreenerGrain.Framework.Helpers.HttpClientHelpers;
using GreenerGrain.Framework.Interfaces;
using GreenerGrain.Framework.Result;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GreenerGrain.Framework.Services
{
    /// <summary>
    /// Implementação do client Rest
    /// </summary>
    public class RestClientServiceBase: IRestClientServiceBase
    {
        private readonly IApiContext _apiContext;

        /// <summary>
        /// Timeout para a chamada, caso não seja informado, utilizará o tempo default de 120 segundos.
        /// </summary>
        public TimeSpan? Timeout { get; set; } = null;

        /// <summary>
        /// Cabeçalho para a request, caso não seja informado, utilizará o Bearer ativo
        /// </summary>
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Construtor
        /// </summary>
        public RestClientServiceBase(IApiContext apiContext)
        {
            this._apiContext = apiContext;
        }

        /// <summary>
        /// Executa um GET Asincrono na URI informada 
        /// </summary>
        public async Task<RestResponse<T>> GetAsync<T>(Uri requestUrl)
        {
            return await InnerCommandAsync<T>(HttpMethod.Get, requestUrl, null);
        }

        /// <summary>
        /// Executa um GET Sincrono na URI informada 
        /// </summary>
        public RestResponse<T> Get<T>(Uri requestUrl)
        {
            return GetAsync<T>(requestUrl).Result;
        }


        /// <summary>
        /// Executa um POST Asincrono na URI informada 
        /// </summary>
        public async Task<RestResponse<T>> PostAsync<T>(Uri requestUrl, StringContent content)
        {
            return await InnerCommandAsync<T>(HttpMethod.Post, requestUrl, content);
        }

        /// <summary>
        /// Executa um POST Sincrono na URI informada 
        /// </summary>
        public RestResponse<T> Post<T>(Uri requestUrl, StringContent content)
        {
            return PostAsync<T>(requestUrl, content).Result;
        }

        /// <summary>
        /// Executa um PUT Asincrono na URI informada 
        /// </summary>
        public async Task<RestResponse<T>> PutAsync<T>(Uri requestUrl, StringContent content)
        {
            return await InnerCommandAsync<T>(HttpMethod.Put, requestUrl, content);
        }

        /// <summary>
        /// Executa um PUT Sincrono na URI informada 
        /// </summary>
        public RestResponse<T> Put<T>(Uri requestUrl, StringContent content)
        {
            return PutAsync<T>(requestUrl, content).Result;
        }


        /// <summary>
        /// Executa um DELETE Asincrono na URI informada 
        /// </summary>
        public async Task<RestResponse<T>> DeleteAsync<T>(Uri requestUrl)
        {
            return await InnerCommandAsync<T>(HttpMethod.Delete, requestUrl, null);
        }

        /// <summary>
        /// Executa um DELETE Sincrono na URI informada 
        /// </summary>
        public RestResponse<T> Delete<T>(Uri requestUrl)
        {
            return DeleteAsync<T>(requestUrl).Result;
        }

        private async Task<RestResponse<T>> InnerCommandAsync<T>(HttpMethod httpMethod, Uri requestUrl, StringContent content)
        {
            using (var handler = new TimeoutHandler
            {
                InnerHandler = new HttpClientHandler()
            })
            {
                using (var httpClient = new HttpClient(handler))
                {
                    if (Headers.Count <= 0)
                    {
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + _apiContext.SecurityContext.JwtToken);
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
                    }
                    else
                    {
                        foreach (var head in Headers)
                        {
                            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(head.Key, head.Value);
                        }
                    }

                    var request = new HttpRequestMessage(httpMethod, requestUrl);

                    if ((httpMethod == HttpMethod.Post) || (httpMethod == HttpMethod.Put))
                    {
                        request.Content = content;
                    }

                    if (Timeout != null)
                    {
                        request.SetTimeout(Timeout);
                    }

                    using (var responseGet = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                    {
                        RestResponse<T> response = new RestResponse<T>();
                        string responseContent = await responseGet.Content.ReadAsStringAsync();

                        if (responseGet.IsSuccessStatusCode)
                        {
                            response.Success = true;
                            response.Result = JsonConvert.DeserializeObject<T>(responseContent);
                        }
                        else
                        {
                            response.Success = false;
                            response.Result = (String.IsNullOrEmpty(responseContent) ? default(T) : JsonConvert.DeserializeObject<T>(responseContent));
                            response.Error = new Error(((int)responseGet.StatusCode).ToString(), responseGet.StatusCode.ToString());
                        }

                        return response;
                    }
                }
            }
        }

    }
}

using GreenerGrain.Framework.Result;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GreenerGrain.Framework.Interfaces
{
    /// <summary>
    /// Interface para implementação do client Rest
    /// </summary>
    public interface IRestClientServiceBase
    {
        /// <summary>
        /// Executa um GET Asincrono na URI informada 
        /// </summary>
        Task<RestResponse<T>> GetAsync<T>(Uri requestUrl);

        /// <summary>
        /// Executa um GET Sincrono na URI informada 
        /// </summary>
        RestResponse<T> Get<T>(Uri requestUrl);

        /// <summary>
        /// Executa um POST Asincrono na URI informada 
        /// </summary>
        Task<RestResponse<T>> PostAsync<T>(Uri requestUrl, StringContent content);

        /// <summary>
        /// Executa um POST Sincrono na URI informada 
        /// </summary>
        RestResponse<T> Post<T>(Uri requestUrl, StringContent content);

        /// <summary>
        /// Executa um PUT Asincrono na URI informada 
        /// </summary>
        Task<RestResponse<T>> PutAsync<T>(Uri requestUrl, StringContent content);

        /// <summary>
        /// Executa um PUT Sincrono na URI informada 
        /// </summary>
        RestResponse<T> Put<T>(Uri requestUrl, StringContent content);

        /// <summary>
        /// Executa um DELETE Asincrono na URI informada 
        /// </summary>
        Task<RestResponse<T>> DeleteAsync<T>(Uri requestUrl);

        /// <summary>
        /// Executa um DELETE Sincrono na URI informada 
        /// </summary>
        RestResponse<T> Delete<T>(Uri requestUrl);
    }
}

using API.Framework.Interfaces;
using GreenerGrain.Framework.Exceptions;
using GreenerGrain.Framework.Field;
using GreenerGrain.Framework.Filtering;
using GreenerGrain.Framework.Result;
using GreenerGrain.Framework.Sorting;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack.Text;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GreenerGrain.Framework.Controllers
{
    /// <summary>
    /// Classe base para implementação de controllers.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        private readonly IApiContext _apiContext;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private const int ReadChunkBufferLength = 4096;


        /// <summary>
        /// Construtor
        /// </summary>
        public ApiBaseController(IApiContext apiContext)
        {
            this._apiContext = apiContext;
            this._recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        /// <summary>
        /// Retorna a requisição origem em formato 'bruto' (string do body).
        /// </summary>
        /// <returns>Requisição origem em formato 'bruto' (string do body)</returns>
        protected string GetRequestContent()
        {
            var request = GetRequestContentAsync().Result;
            return request;
        }

        /// <summary>
        /// Retorna a requisição convertida para um tipo específico.
        /// </summary>
        /// <typeparam name="T">Tipo que a requisição deve ser convertida.</typeparam>
        /// <returns>Requisição convertida para o tipo T</returns>
        protected T GetRequestContent<T>()
        {
            var request = GetRequestContentAsync().Result;
            T result = JsonConvert.DeserializeObject<T>(request);
            return result;
        }

        /// <summary>
        /// Monta um envelope padrão de resposta para um resultado com sucesso.
        /// </summary>
        /// <typeparam name="R">Tipo do resultado.</typeparam>
        /// <param name="result">Objeto resultado.</param>
        /// <returns>Retorna um JsonResult com o envelope padrão de resposta.</returns>
        protected IActionResult JsonResponseMessage<R>(R result)
        {
            object resp;

            if (_apiContext.PagingContext.IsPaginated)
            {
                ApiPaginatedResponse<R> apiPaginatedResponse = new ApiPaginatedResponse<R>();
                var contextResponsePaging = _apiContext.PagingContext.ResponsePaging;

                apiPaginatedResponse.Success = true;
                apiPaginatedResponse.Result = result;
                apiPaginatedResponse.Paging = new Pagination(contextResponsePaging.CurrentRecord,
                                                             contextResponsePaging.TotalRecords,
                                                             contextResponsePaging.PageSize);
                resp = apiPaginatedResponse;
            }
            else
            {
                ApiResponse<R> apiResponse = new ApiResponse<R>
                {
                    Success = true,
                    Result = result
                };

                resp = apiResponse;
            }

            JsonResult jsonResult = new JsonResult(resp);
            return jsonResult;
        }

        /// <summary>
        /// Monta um envelope padrão de resposta para um resultado com erro.
        /// </summary>
        /// <param name="ex">Exception com o erro ocorrido.</param>
        /// <returns>Retorna um JsonResult com o envelope padrão de resposta.</returns>
        protected IActionResult ErrorResponseMessage(Exception ex)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();

            if (_apiContext.PagingContext.IsPaginated)
            {
                ApiPaginatedResponse<object> apiPaginatedResponse = new ApiPaginatedResponse<object>
                {
                    Paging = null
                };

                apiResponse = apiPaginatedResponse;
            }

            apiResponse.Success = false;
            apiResponse.Result = null;

            BaseException baseException = ex as BaseException;
            if (baseException == null || baseException.HttpStatusCode == null)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                apiResponse.AddError(new Error("", ex));
            }
            else
            {
                Response.StatusCode = baseException.HttpStatusCode.Value;
                apiResponse.AddError(new Error(baseException.ErrorCode, baseException));
            }

            if (_apiContext.Errors != null)
            {
                foreach (var error in _apiContext.Errors)
                {
                    apiResponse.AddError(error);
                }
            }

            JsonResult jsonResult = new JsonResult(apiResponse);
            return jsonResult;
        }

        /// <summary>
        /// Invoca um método do serviço retornando a resposta no envelope padrão.
        /// </summary>
        /// <typeparam name="T">Tipo do payload.</typeparam>
        /// <typeparam name="R">Tipo da resposta.</typeparam>
        /// <param name="serviceMethod">Método do serviço a ser executado.</param>
        /// <param name="payload">Parâmetros do método.</param>
        /// <returns>Retorna um JsonResult com o envelope padrão de resposta.</returns>
        protected IActionResult ServiceInvoke<T, R>(Func<T, R> serviceMethod, T payload)
        {
            R serviceMethodResult;
            IActionResult httpResponseMessage;

            try
            {
                HandleQueryParameters();
                serviceMethodResult = serviceMethod(payload);
                httpResponseMessage = JsonResponseMessage<R>(serviceMethodResult);
            }
            catch (Exception ex)
            {
                httpResponseMessage = ErrorResponseMessage(ex);
            }

            // Telemetria do insights
            RegisterTelemetry<T>(httpResponseMessage, payload);

            return httpResponseMessage;
        }

        /// <summary>
        /// Invoca um método do serviço retornando a resposta no envelope padrão.
        /// </summary>
        /// <typeparam name="R">Tipo da resposta.</typeparam>
        /// <param name="serviceMethod">Método do serviço a ser executado.</param>
        /// <returns>Retorna um JsonResult com o envelope padrão de resposta.</returns>
        protected IActionResult ServiceInvoke<R>(Func<R> serviceMethod)
        {
            R serviceMethodResult;
            IActionResult httpResponseMessage;

            try
            {
                HandleQueryParameters();
                serviceMethodResult = serviceMethod();
                httpResponseMessage = JsonResponseMessage<R>(serviceMethodResult);
            }
            catch (Exception ex)
            {
                httpResponseMessage = ErrorResponseMessage(ex);
            }

            // Telemetria do insights
            RegisterTelemetry(httpResponseMessage);

            return httpResponseMessage;
        }

        private void RegisterTelemetry<T>(IActionResult httpResponseMessage, T payload)
        {
            if (_apiContext.EnableTelemetry)
            {
                string request = FormatRequest(HttpContext);
                string response = JsonConvert.SerializeObject(httpResponseMessage, Formatting.Indented);

                request += $"Request Payload: {Environment.NewLine} {JsonConvert.SerializeObject(payload, Formatting.Indented)}";
                
                SetTelemetry(HttpContext, "request", request);
                SetTelemetry(HttpContext, "response", response);
            }
        }

        private void RegisterTelemetry(IActionResult httpResponseMessage)
        {
            if (_apiContext.EnableTelemetry)
            {
                string request = FormatRequest(HttpContext);
                string response = JsonConvert.SerializeObject(httpResponseMessage, Formatting.Indented);

                SetTelemetry(HttpContext, "request", request);
                SetTelemetry(HttpContext, "response", response);
            }
        }


        /// <summary>
        /// Retorna a requisição origem em formato 'bruto' (string do body).
        /// </summary>
        /// <returns>Requisição origem em formato 'bruto' (string do body)</returns>
        private async Task<string> GetRequestContentAsync()
        {
            string request = "";
            using (StreamReader sr = new StreamReader(Request.Body))
            {
                request = await sr.ReadToEndAsync();
            }

            return request;
        }

        /// <summary>
        /// Retorna se um HTTP Header existe na requisição atual.
        /// </summary>
        /// <param name="headerName">Nome do header a ser procurado</param>
        /// <returns>True se o header existe na requisição atual. False, caso contrário.</returns>
        private bool HeaderExists(string headerName)
        {
            foreach (var h in Request.Headers)
            {
                if (h.Key == headerName) return true;
            }
            return false;
        }

        /// <summary>
        /// Efetua o tramento dos query parameters
        /// </summary>
        private void HandleQueryParameters()
        {
            string queryString = Request?.QueryString.ToString();
            if (string.IsNullOrEmpty(queryString))
            {
                return;
            }

            int n = 0;

            var queryDictionary = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(queryString);
            foreach (var item in queryDictionary)
            {
                string queryValue = string.Empty;
                string[] values = null;

                switch (item.Key.ToLowerInvariant())
                {
                    case "page":
                        n = 0;
                        if (int.TryParse(item.Value, out n))
                        {
                            _apiContext.PagingContext.RequestPaging.Page = n;
                        }
                        if (n <= 0)
                        {
                            throw new BadRequestException("Invalid value for 'page'.");
                        }
                        break;

                    case "pagesize":
                        n = 0;
                        if (int.TryParse(item.Value, out n))
                        {
                            _apiContext.PagingContext.RequestPaging.PageSize = n;
                        }
                        if (n <= 0)
                        {
                            throw new BadRequestException("Invalid value for 'pageSize'.");
                        }
                        break;

                    case "sort":
                        if (!string.IsNullOrEmpty(item.Value))
                        {
                            queryValue = item.Value;

                            foreach (var itemValue in queryValue.Split(","))
                            {
                                string key = string.Empty;
                                string value = string.Empty;

                                try
                                {
                                    key = itemValue.Split(":")[0];
                                }
                                catch
                                {
                                    throw new BadRequestException("Invalid value for sort key.");
                                }

                                if (string.IsNullOrEmpty(key))
                                {
                                    throw new BadRequestException("Invalid value for sort key.");
                                }

                                try
                                {
                                    value = itemValue.Split(":")[1];
                                }
                                catch
                                {
                                    value = "ASC";
                                }

                                SortKey sortkey = new SortKey(key.ToUpper(), value.ToUpper());

                                if (sortkey.direction != "ASC" && sortkey.direction != "DESC")
                                {
                                    throw new BadRequestException("Invalid value for sort direction on " + sortkey.key);
                                }

                                _apiContext.SortingContext.sortKeys.Add(sortkey);
                            }
                        }

                        break;
                    case "fields":
                        if (!string.IsNullOrEmpty(item.Value))
                        {
                            queryValue = item.Value;
                            values = queryValue.Split(",").Where(x => !string.IsNullOrEmpty(x)).ToArray();

                            FieldKey fieldKey = new FieldKey(item.Key, values);
                            _apiContext.FieldContext.fieldKeys.Add(fieldKey);
                        }

                        break;
                    default:
                        if (string.IsNullOrEmpty(item.Key))
                        {
                            throw new BadRequestException("Invalid value for filter key.");
                        }

                        var filterKey = new FilterKey(item.Key, item.Value);
                        _apiContext.FilteringContext.filterKeys.Add(filterKey);

                        break;
                }
            }
        }

        private void SetTelemetry(HttpContext context, string key, string value)
        {
            RequestTelemetry telemetry = context?.Features.Get<RequestTelemetry>();
            if (telemetry != null)
                telemetry.Properties[key] = value;
        }

        private string FormatRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            return
                    $"Host: {request.Host} {Environment.NewLine}" +
                    $"Path: {request.Path} {Environment.NewLine}" +
                    $"QueryString: {request.QueryString} {Environment.NewLine}" +
                    $"Headers: {Environment.NewLine} {GetHeaders()} {Environment.NewLine}";
        }

        private string GetHeaders()
        {
            return JsonConvert.SerializeObject(HttpContext.Request.Headers, Formatting.Indented);
        }
    }
}

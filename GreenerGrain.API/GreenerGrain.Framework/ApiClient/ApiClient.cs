using System;
using GreenerGrain.Framework.Helpers.HttpClientHelpers;
using GreenerGrain.Framework.Interfaces;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GreenerGrain.Framework.ApiClient
{
    public partial class ApiRestClient
    {
        #region Fields

        private readonly HttpClient _httpClient;
        private readonly IJwtUtil _jwtUtil;
        private Uri _baseEndpoint { get; set; }
        private string _token { get; set; }

        public Uri BaseEndpoint
        {
            get { return _baseEndpoint; }
            set { _baseEndpoint = value ?? throw new ArgumentNullException("baseEndpoint"); }
        }

        public string Token
        {
            get { return _token; }
            set { _token = value ?? throw new ArgumentNullException("token"); }
        }

        private static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }

        #endregion

        #region Constructor

        public ApiRestClient(IJwtUtil jwtUtil)
        {
            var handler = new TimeoutHandler
            {
                InnerHandler = new HttpClientHandler()
            };

            _httpClient = new HttpClient(handler);
            _jwtUtil = jwtUtil;
        }

        #endregion

        #region Methods

        public Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            var endpoint = new Uri(_baseEndpoint, relativePath);
            var uriBuilder = new UriBuilder(endpoint)
            {
                Query = queryString
            };
            return uriBuilder.Uri;
        }

        public void AddAuthenticationHeader()
        {
            var access_token = _token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        }

        public async Task<T> GetAsync<T>(Uri requestUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.SetTimeout(TimeSpan.FromSeconds(240));
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        public async Task<T> PostAsync<T>(Uri requestUrl, StringContent content)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            request.SetTimeout(TimeSpan.FromSeconds(240));
            var response = await _httpClient.PostAsync(requestUrl, content);
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        #endregion
    }
}

using GreenerGrain.Framework.Exceptions;
using GreenerGrain.Domain.Entities;
using GreenerGrain.Domain.Enumerators;
using GreenerGrain.Provider.Interfaces.Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net.Http;

namespace GreenerGrain.Provider.Implementations.Google
{
    public class GoogleClientBuilder : IGoogleCredentialBuilder
    {
        #region Fields

        private readonly IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());

        private static readonly string[] Scopes = {
            CalendarService.Scope.Calendar,
            CalendarService.Scope.CalendarEvents,
            CalendarService.Scope.CalendarReadonly,
            CalendarService.Scope.CalendarSettingsReadonly,
            CalendarService.Scope.CalendarEventsReadonly
        };

        #endregion

        #region Constructor

        public GoogleClientBuilder(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        #endregion

        #region Methods

        public CalendarService CreateCalendarService(InstitutionProviderSettings institutionProvider)
        {
            var credential = GetGoogleCredential(institutionProvider);

            return new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "VirtualScheduling"
            });
        }        

        #endregion

        #region Private Methods

        private GoogleCredential GetGoogleCredential(InstitutionProviderSettings institutionProvider)
        {
            

            ValidateGoogleProvider(institutionProvider);

            var key = $"GoogleCredential_{institutionProvider.InstitutionId}_{institutionProvider.CredentialEmail}";
            return memoryCache.GetOrCreate(key, e =>
            {
                e.SlidingExpiration = TimeSpan.FromMinutes(10);

                var client = new HttpClient();                
                var response = client.GetAsync(institutionProvider.JsonKey).Result;
                var content = response.Content.ReadAsStreamAsync().Result;

                var credential = GoogleCredential.FromStream(content)
                    .CreateScoped(Scopes)
                    .CreateWithUser(institutionProvider.CredentialEmail);               

                return credential;
            });
        }

        private void ValidateGoogleProvider(InstitutionProviderSettings provider)
        {
            if (provider?.CredentialEmail is null)
            {
                throw new BadRequestException(InstitutionProviderErrors.CredentialEmailNotFound);
            }

            if (provider?.JsonKey is null)
            {
                throw new BadRequestException(InstitutionProviderErrors.JsonKeyNotFound);
            }
        }

        #endregion
    }
}
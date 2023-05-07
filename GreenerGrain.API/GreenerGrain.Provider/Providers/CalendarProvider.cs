using GreenerGrain.Framework.Exceptions;
using GreenerGrain.Domain.Entities;
using GreenerGrain.Domain.Enumerators;
using GreenerGrain.Domain.Payloads;
using GreenerGrain.Provider.Implementations.Google;
using GreenerGrain.Provider.Interfaces;
using GreenerGrain.Provider.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace GreenerGrain.Provider.Providers
{
    public class CalendarProvider
    {
        ICalendarIntegration calendarIntegration;

        public CalendarProvider(IServiceProvider serviceProvider, InstitutionProviderSettings institutionSettings, IConfiguration configuration)
        {

            switch (institutionSettings.ProviderCode)
            {
                case "GOOGLE_CALENDAR":
                    calendarIntegration = new GoogleCalendarIntegration(serviceProvider, institutionSettings);
                    break;
                default:
                    throw new InternalException(ProviderErrors.ProviderNotFound);
            }
        }

        public CalendarEventModel CreateEvent(CreateCalendarEventPayload payload)
        {
            return calendarIntegration.CreateEvent(payload);
        }
    }
}

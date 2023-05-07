using GreenerGrain.Domain.Entities;
using GreenerGrain.Domain.Payloads;
using GreenerGrain.Provider.Interfaces;
using GreenerGrain.Provider.Interfaces.Google;
using GreenerGrain.Provider.Models;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenerGrain.Provider.Implementations.Google
{
    public class GoogleCalendarIntegration : ICalendarIntegration
    {
        private readonly IGoogleCredentialBuilder googleClientBuilder;
        private readonly InstitutionProviderSettings _institutionSettings;
        private readonly CalendarService _calendarService;
        
        private const string CONFERENCETYPE = "hangoutsMeet";
        public static readonly List<string> ALLOWEDCONFERENCESOLUTIONTYPES = new List<string> {
            "eventHangout",
            "eventNamedHangout",
            "hangoutsMeet"
        };

        public GoogleCalendarIntegration(
              IServiceProvider serviceProvider
            , InstitutionProviderSettings institutionSettings)
        {
            _institutionSettings = institutionSettings;
            googleClientBuilder = serviceProvider.GetService<IGoogleCredentialBuilder>() ?? throw new ArgumentNullException(nameof(IGoogleCredentialBuilder));
            _calendarService = googleClientBuilder.CreateCalendarService(this._institutionSettings);
        }

        public CalendarEventModel CreateEvent(CreateCalendarEventPayload payload)
        {

            var calendarId = CreateCalendarIfNotExists(payload.CalendarName);

            if (calendarId is null)
            {
                return null;
            }
            
            SetEventDateTime(payload, out DateTime start, out DateTime end);

            var newEvent = new Event()
            {
                Summary = payload.EventSubject,
                
                Start = new EventDateTime()
                {
                    DateTimeRaw = start.ToString("yyyy-MM-ddTHH:mm:ss.ffzzz"),
                    TimeZone = payload.CalendarTimeZone
                },

                End = new EventDateTime()
                {
                    DateTimeRaw = start.ToString("yyyy-MM-ddTHH:mm:ss.ffzzz"),
                    TimeZone = payload.CalendarTimeZone
                },

                ConferenceData = new ConferenceData()
                {
                    CreateRequest = new CreateConferenceRequest
                    {
                        Status = new ConferenceRequestStatus
                        {
                            StatusCode = "success"
                        },
                        ConferenceSolutionKey = new ConferenceSolutionKey
                        {
                            Type = CONFERENCETYPE
                        },
                        RequestId = $"requestMeet_{start.Ticks}"
                    }
                }
            };

            //add attendees to the meet
            if (payload.EventAttendees.Any())
            {
                newEvent.Attendees = payload.EventAttendees.Select(x => new EventAttendee
                {
                    Email = x.Email,
                    DisplayName = x.DisplayName
                }).ToList();
            }

            EventsResource.InsertRequest request = _calendarService.Events.Insert(newEvent, calendarId);
            request.ConferenceDataVersion = 1; //conference data support - google meet
            request.SendUpdates = EventsResource.InsertRequest.SendUpdatesEnum.All;

            Event createdEvent = request.Execute();
            if (createdEvent != null)
            {
                return new CalendarEventModel
                {
                    Id = createdEvent.Id,
                    Summary = createdEvent.Summary,
                    Start = createdEvent.Start.DateTime,
                    End = createdEvent.End.DateTime,
                    HtmlLink = createdEvent.HtmlLink,
                    HangoutLink = createdEvent.HangoutLink
                };
            }

            return null;
        }

        private static void SetEventDateTime(CreateCalendarEventPayload payload, out DateTime start, out DateTime end)
        {

            if (payload.StartTime == null)
            {
                start = DateTime.UtcNow;
                end = start.AddMinutes(10);
            }
            else
            {
                TimeZoneInfo instanceTimezone = TimeZoneInfo.FindSystemTimeZoneById(payload.CalendarTimeZone);

                var agendaUtcOffset = instanceTimezone.BaseUtcOffset;

                var serverUtcOffset = TimeZoneInfo.Local.BaseUtcOffset;

                start = payload.StartTime.Value.ToUniversalTime();
                start = start.AddTicks(serverUtcOffset.Ticks-agendaUtcOffset.Ticks);

                end = payload.EndTime.Value.ToUniversalTime();
                end = end.AddTicks(serverUtcOffset.Ticks-agendaUtcOffset.Ticks);
            }
        }

        private string CreateCalendarIfNotExists(string calendarName)
        {
            var calendarList = _calendarService.CalendarList.List().Execute();
            var existingCalendar = calendarList.Items.FirstOrDefault(x => x.Summary == calendarName);
            
            if (existingCalendar is not null)
            {
                return existingCalendar.Id;
            }

            var request = _calendarService.Calendars.Insert(new Calendar
            {
                Summary = calendarName,
                ConferenceProperties = new ConferenceProperties
                {
                    AllowedConferenceSolutionTypes = ALLOWEDCONFERENCESOLUTIONTYPES
                }                
            });

            var calendar = request.Execute();
            return calendar.Id;
        }
        
    }
}

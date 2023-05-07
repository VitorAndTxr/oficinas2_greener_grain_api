using System;
using System.Collections.Generic;

namespace GreenerGrain.Domain.Payloads
{
    public class CreateCalendarEventPayload
    {
        public string EventSubject { get; set; } //Agendamento TJSP - Protocolo Numero #321
        public string ProtocolNumber { get; set; }
        public string CalendarName { get; set; }
        public string CalendarTimeZone { get; set; }
        public Guid InstitutionId { get; set; }
        public DateTime? StartTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;

        public IList<EventAttendeePayload> EventAttendees { get; set; } = new List<EventAttendeePayload>();
    }

    public class EventAttendeePayload
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
    }
}

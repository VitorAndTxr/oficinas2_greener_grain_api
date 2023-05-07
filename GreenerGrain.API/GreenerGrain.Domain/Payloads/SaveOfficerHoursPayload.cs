using System;
using System.Collections.Generic;

namespace GreenerGrain.Domain.Payloads
{
    public class SaveOfficerHoursPayload
    {
        public Guid ServiceDeskId { get; set; }
        public IList<HoursPayload> Hours { get; set; }
    }

    public class HoursPayload
    {
        public Guid? Id { get; set; }
        public Guid InstitutionServiceLocationId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}

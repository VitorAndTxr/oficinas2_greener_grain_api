using GreenerGrain.Domain.Payloads;
using GreenerGrain.Provider.Models;
using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenerGrain.Provider.Interfaces
{
    public interface ICalendarIntegration
    {
        CalendarEventModel CreateEvent(CreateCalendarEventPayload payload);

    }
}

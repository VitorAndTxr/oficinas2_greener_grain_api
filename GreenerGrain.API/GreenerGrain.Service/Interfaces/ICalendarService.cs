using GreenerGrain.Domain.Payloads;
using GreenerGrain.Domain.ViewModels;

namespace GreenerGrain.Service.Interfaces
{
    public interface ICalendarService
    {
        CalendarEventViewModel CreateCalendarEvent(CreateCalendarEventPayload payload);
    }
}

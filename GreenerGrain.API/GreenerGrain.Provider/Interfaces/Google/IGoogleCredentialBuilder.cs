using GreenerGrain.Domain.Entities;
using Google.Apis.Calendar.v3;

namespace GreenerGrain.Provider.Interfaces.Google
{
    public interface IGoogleCredentialBuilder
    {
        CalendarService CreateCalendarService(InstitutionProviderSettings institutionProvider);
    }
}

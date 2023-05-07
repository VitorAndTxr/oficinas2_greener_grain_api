using System;

namespace GreenerGrain.Domain.ViewModels
{
    public class OfficerHourViewModel
    {
        public Guid Id { get; set; }
        public Guid AttendantServiceId { get; private set; }
        public Guid PlaceId { get; private set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }

        public virtual ServiceDeskOfficerViewModel AttendantService { get; set; }
        public virtual InstitutionServiceLocationViewModel Place { get; set; }

    }
}

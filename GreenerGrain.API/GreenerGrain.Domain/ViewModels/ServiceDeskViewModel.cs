using GreenerGrain.Domain.Enumerators;
using System;
using System.Collections.Generic;

namespace GreenerGrain.Domain.ViewModels
{
    public class ServiceDeskViewModel
    {
        public Guid Id { get; set; }
        public ServiceDeskTypeEnum ServiceDeskTypeId { get; set; }
        public Guid InstitutionId { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string CalendarName { get; set; }
        public string CalendarTimeZone { get; set; }
        public int MeetDurationTime { get; set; }

        public virtual ServiceDeskTypeViewModel ServiceDeskType { get; set; }
        public virtual List<ServiceDeskOpeningHoursViewModel> ServiceDeskOpeningHours { get; set; } = new List<ServiceDeskOpeningHoursViewModel>();

    }
}

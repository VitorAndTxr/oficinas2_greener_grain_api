using System;

namespace GreenerGrain.Domain.ViewModels
{
    public class OfficerHoursViewModel
    {
        public Guid Id { get; set; }
        public Guid ServiceDeskOfficerId { get; set; }
        public Guid InstitutionServiceLocationId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }        
    }
}

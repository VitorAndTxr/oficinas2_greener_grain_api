using GreenerGrain.Domain.Entities;
using System;

namespace GreenerGrain.Domain.ViewModels
{
    public class ServiceDeskOpeningHoursViewModel
    {
        public Guid Id { get; set; }
        public Guid ServiceDeskId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}

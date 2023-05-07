using GreenerGrain.Framework.Security;
using GreenerGrain.Domain.Entities;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Provider.Models;

namespace GreenerGrain.Service.AutoMapper
{
    public class DomainToViewModelMappingProfile : global::AutoMapper.Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Appointment, AppointmentViewModel>();

            CreateMap<AppointmentQueue, AppointmentQueueViewModel>();

            CreateMap<OfficerHour, OfficerHourViewModel>();
            
            CreateMap<OfficerPause, OfficerPauseViewModel>();
            
            CreateMap<ServiceDeskOfficer, ServiceDeskOfficerViewModel>();
            
            CreateMap<CalendarEventModel, CalendarEventViewModel>();
            
            CreateMap<Customer, CustomerViewModel>();
            
            CreateMap<InstitutionServiceLocation, InstitutionServiceLocationViewModel>();

            CreateMap<ServiceDeskType, ServiceDeskTypeViewModel>();

            CreateMap<ServiceDesk, ServiceDeskViewModel>();

            CreateMap<ServiceDeskOpeningHours, ServiceDeskOpeningHoursViewModel>();

            CreateMap<OfficerHour, OfficerHoursViewModel>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToString(@"hh\:mm")))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToString(@"hh\:mm")));

            CreateMap<InstitutionReport, ReportLinkViewModel>();


            CreateMap<GreenerGrain.Domain.Entities.Profile, ProfileViewModel>();
        }
    }
}

using API.Framework.Interfaces;
using AutoMapper;
using GreenerGrain.Framework.Database.EfCore.Interface;
using GreenerGrain.Framework.Exceptions;
using GreenerGrain.Framework.Services;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.Enumerators;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Service.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GreenerGrain.Service.Services
{
    public class ServiceDeskService : ServiceBase, IServiceDeskService
    {
        #region Fields

        private readonly IApiContext _apiContext;
        private readonly IMapper _mapper;
        private readonly IServiceDeskRepository _serviceDeskRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IServiceDeskOfficerRepository _serviceDeskOfficerRepository;
        private readonly IOfficerHourService _officerHourService;


        private readonly IServiceDeskOpeningHoursRepository _serviceDeskOpeningHoursRepository;
        private readonly IUnitOfWork _unitOfWork;



        #endregion

        #region Constructor

        public ServiceDeskService(
            IApiContext apiContext
            , IMapper mapper
            , IAppointmentRepository appointmentRepository
            , IServiceDeskRepository serviceDeskRepository
            , IServiceDeskOfficerRepository serviceDeskOfficerRepository
            , IServiceDeskOpeningHoursRepository serviceDeskOpeningHoursRepository
            , IOfficerHourService officerHourService
            , IUnitOfWork unitOfWork) : base(apiContext)
        {
            _apiContext = apiContext;
            _mapper = mapper;
            _serviceDeskRepository = serviceDeskRepository;
            _appointmentRepository = appointmentRepository;

            _serviceDeskOpeningHoursRepository = serviceDeskOpeningHoursRepository;
            _unitOfWork = unitOfWork;
            _serviceDeskOfficerRepository = serviceDeskOfficerRepository;
            _officerHourService = officerHourService;
        }

        #endregion

        public ServiceDeskViewModel GetById(Guid id)
        {
            var serviceDesk = _serviceDeskRepository.GetByIdAsync(id).Result;

            var result = _mapper.Map<ServiceDeskViewModel>(serviceDesk);
            return result;
        }

        public ServiceDeskViewModel GetByCode(string code)
        {
            var serviceDesk = _serviceDeskRepository.GetByCodeAsync(code).Result;
            if (serviceDesk == null)
            {
                throw new BadRequestException(ServiceDeskError.ServiceDeskDoesNotExist);
            }

            if (serviceDesk.ServiceDeskTypeId == ServiceDeskTypeEnum.VirtualQueue)
            {
                var isOpen = false;

                var oppeningHours = _serviceDeskOpeningHoursRepository
                    .GetByServiceDeskAndDayOfWeek(serviceDesk.Id, DateTime.Now.DayOfWeek).Result;

                if (oppeningHours.Count > 0)
                {
                    for (int i = 0; i < oppeningHours.Count; i++)
                    {
                        var oppeningHour = DateTime.Today.AddTicks(oppeningHours[i].StartTime.Ticks);
                        var closeHour = DateTime.Today.AddTicks(oppeningHours[i].EndTime.Ticks);

                        TimeZoneInfo instanceTimezone = TimeZoneInfo.FindSystemTimeZoneById(serviceDesk.CalendarTimeZone);

                        var queueUtcOffset = instanceTimezone.BaseUtcOffset;

                        var currentClientTime = DateTime.UtcNow.AddTicks(queueUtcOffset.Ticks);

                        if (oppeningHour <= currentClientTime && currentClientTime <= closeHour)
                        {
                            isOpen = true;
                            break;
                        }
                    }
                }

                if (!isOpen)
                {
                    throw new BadRequestException(ServiceDeskError.ServiceDeskIsNotOpen);
                }
            }

            var result = _mapper.Map<ServiceDeskViewModel>(serviceDesk);
            return result;
        }

        public IList<ServiceDeskViewModel> ListScheduleServices(Guid institutionId)
        {
            var listOfServiceDesk = _serviceDeskRepository
                .GetByInstitutionAndServiceTypeAsync(institutionId, ServiceDeskTypeEnum.ScheduleAppointment).Result;

            var result = _mapper.Map<IList<ServiceDeskViewModel>>(listOfServiceDesk);
            return result;
        }

        public List<ServiceDeskOfficerAvaliabilityViewModel> GetServiceDeskSchedulingAvailability(Guid serviceDeskId)
        {
            // Retrieve the service desk
            var serviceDesk = _serviceDeskRepository.GetByIdAsync(serviceDeskId).Result;
            if (serviceDesk == null)
            {
                throw new BadRequestException(ServiceDeskError.ServiceDeskDoesNotExist);
            }

            // Retrieve the service desk officer
            var serviceDeskOfficer = _serviceDeskOfficerRepository.GetAsync(x => x.ServiceDeskId == serviceDeskId).Result.FirstOrDefault();
            if (serviceDeskOfficer == null)
            {
                throw new BadRequestException(OfficerError.OfficerDoesNotExist);
            }

            // Retrieve the officer's scheduling opening hours
            var schedulingOpenningHours = _officerHourService.GetOfficerHoursByServiceDeskOfficerId(serviceDeskOfficer.Id);
            if (schedulingOpenningHours.Count == 0)
            {
                throw new BadRequestException(ServiceDeskError.ServiceDeskNoTimeAvailable);
            }


            // Initialize variables
            var currentDate = DateTime.Now;
            List<ServiceDeskOfficerAvaliabilityViewModel> schedulingAvaliabilities = new();

            // Loop through the next 5 days
            while (schedulingAvaliabilities.Count < 5)
            {
                var daySchedulingOpenningHours = schedulingOpenningHours
                    .Where(x => x.DayOfWeek == currentDate.DayOfWeek)
                    .ToList();

                if (daySchedulingOpenningHours.Any())
                {
                    var schedulingAvaliability = new ServiceDeskOfficerAvaliabilityViewModel
                    {
                        Order = schedulingAvaliabilities.Count,
                        DateLabel = $"{currentDate.ToString("dddd", CultureInfo.CreateSpecificCulture("pt-BR"))} {currentDate.ToString("dd/MM")}",
                        AvaliableTimes = new List<string>()
                    };

                    foreach (var daySchedulingOpenningHour in daySchedulingOpenningHours)
                    {
                        if (TimeSpan.TryParse(daySchedulingOpenningHour.StartTime, out TimeSpan startTimeSpan) &&
                            TimeSpan.TryParse(daySchedulingOpenningHour.EndTime, out TimeSpan endTimeSpan))
                        {
                            var currentTime = currentDate.Date.AddTicks(startTimeSpan.Ticks);
                            var endTime = currentDate.Date.AddTicks(endTimeSpan.Ticks);

                            //get all hours that already has an appointment scheduled.
                            var sheduledAppointments = _appointmentRepository
                                .GetAsync(x => x.ServiceDeskId == serviceDeskId && x.Date >= currentTime && x.Date <= endTime)
                                .Result;

                            while (currentTime < endTime)
                            {
                                if (currentTime > DateTime.Now)
                                {
                                    //dont make it available if this currentTime has an appointment already
                                    if (!sheduledAppointments.Any(x=> x.Date == currentTime))
                                    {
                                        schedulingAvaliability.AvaliableTimes.Add(currentTime.ToString("yyyy-MM-dd HH:mm"));
                                    }
                                }

                                currentTime = currentTime.AddTicks(serviceDesk.GetMeetDuration());
                            }

                        }
                    }


                    if (schedulingAvaliability.AvaliableTimes.Any())
                    {
                        schedulingAvaliabilities.Add(schedulingAvaliability);
                    }
                }

                currentDate = currentDate.AddDays(1);
            }

            return schedulingAvaliabilities;
        }

        public WaitingTimeViewModel GetQueueAverageWaitingTime(Guid serviceDeskId)
        {
            // Initialize the waiting time view model with a default value of 2 minutes
            WaitingTimeViewModel waitingTimeViewModel = new WaitingTimeViewModel()
            {
                AverageWaitingMinutes = 2
            };

            // Get the last five attended appointments for the given service desk
            var lastAttendedAppointments = _appointmentRepository.GetLastFiveAttendedByServiceDeskId(serviceDeskId).Result;

            TimeSpan totalWaitingTime = TimeSpan.Zero;

            // Calculate the total waiting time for all attended appointments
            foreach (var appointment in lastAttendedAppointments)
            {
                // Get the interval between the queue entrance and attendance hours
                var interval = appointment.AppointmentQueue.QueueAttendHour - appointment.AppointmentQueue.QueueEntranceHour;

                // Only add the interval to the total waiting time if it is not null
                if (interval != null)
                {
                    totalWaitingTime = totalWaitingTime.Add((TimeSpan)interval);
                }
            }

            // Calculate the average waiting time if the total waiting time is not zero
            if (totalWaitingTime != TimeSpan.Zero)
            {
                // Divide the total waiting time by the number of attended appointments
                totalWaitingTime = totalWaitingTime / lastAttendedAppointments.Count;

                // Update the waiting time view model with the average waiting time, rounded up to the nearest minute
                if (totalWaitingTime.Minutes > 2)
                {
                    waitingTimeViewModel.AverageWaitingMinutes = totalWaitingTime.Minutes + 1;
                }
            }

            // Return the waiting time view model
            return waitingTimeViewModel;
        }

    }
}

using API.Framework.Interfaces;
using AutoMapper;
using GreenerGrain.Framework.Database.EfCore.Interface;
using GreenerGrain.Framework.Exceptions;
using GreenerGrain.Framework.Services;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.Entities;
using GreenerGrain.Domain.Enumerators;
using GreenerGrain.Domain.Payloads;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenerGrain.Service.Services
{
    public class AppointmentService : ServiceBase, IAppointmentService
    {
        private readonly IApiContext _apiContext;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IServiceDeskService _serviceDeskService;
        private readonly IServiceDeskOfficerService _serviceDeskOfficerService;
        private readonly IServiceDeskOfficerRepository _serviceDeskOfficerrepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICalendarService _calendarService;

        private List<Guid> _busyServiceDesks = new List<Guid>();

        public AppointmentService(
            IApiContext apiContext
            , IAppointmentRepository appointmentRepository
            , IServiceDeskService serviceDeskService
            , IServiceDeskOfficerService serviceDeskOfficerService
            , IServiceDeskOfficerRepository serviceDeskOfficerrepository
            , ICustomerRepository customerRepository
            , ICalendarService calendarService
            , IMapper mapper
            , IUnitOfWork unitOfWork) : base(apiContext)
        {
            _apiContext = apiContext;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _appointmentRepository = appointmentRepository;
            _serviceDeskService = serviceDeskService;
            _serviceDeskOfficerService = serviceDeskOfficerService;
            _serviceDeskOfficerrepository = serviceDeskOfficerrepository;
            _customerRepository = customerRepository;
            _calendarService = calendarService;
        }

        public AppointmentViewModel GetById(Guid id)
        {
            var appointment = _appointmentRepository.GetByIdAsync(id).Result;

            if (appointment == null)
            {
                throw new BadRequestException(AppointmentError.NotFoundEntity);
            }

            var viewModel = _mapper.Map<AppointmentViewModel>(appointment);
            return viewModel;
        }

        public IList<AppointmentViewModel> ListByQueueId(AppointmentListPayload payload)
        {
            PagingValidation();

            var appointments = _appointmentRepository.GetAllByServiceDeskIdAsync(payload.QueueId).Result;

            var result = _mapper.Map<IList<AppointmentViewModel>>(appointments);
            return result;
        }

        public AppointmentViewModel CreateAppointment(CreateAppointmentPayload payload)
        {
            ServiceDeskViewModel serviceDesk = _serviceDeskService.GetById(payload.ServiceDeskId);
            if (serviceDesk == null)
            {
                throw new BadRequestException(AppointmentError.ServiceDeskNotExist);
            }

            return serviceDesk.ServiceDeskTypeId switch
            {
                ServiceDeskTypeEnum.VirtualQueue => InsertAppointmentInVirtualQueue(payload),
                ServiceDeskTypeEnum.ScheduleAppointment => InsertAppointmentInScheduling(payload, serviceDesk),
                _ => throw new BadRequestException(AppointmentError.ServiceDeskTypeDoesNotExist),
            };
        }

        public AppointmentViewModel StartAppointmentQueue(Guid appointmentId)
        {
            var officerId = _apiContext.SecurityContext.Account.Id;
            var institutionId = _apiContext.SecurityContext.Account.InstitutionId;

            var appointment = _appointmentRepository.GetWithAppointmentQueueByIdAsync(appointmentId).Result;
            var serviceDesk = _serviceDeskService.GetById(appointment.ServiceDeskId);

            TimeZoneInfo instanceTimezone = TimeZoneInfo.FindSystemTimeZoneById(serviceDesk.CalendarTimeZone);

            var queueUtcOffset = instanceTimezone.BaseUtcOffset;

            if (_serviceDeskOfficerService.IsOfficer(serviceDesk.Id, officerId))
            {
                CalendarEventViewModel calendarEventResponse = InvokeCreateEvent(
                    serviceDesk.CalendarName, 
                    serviceDesk.CalendarTimeZone, 
                    appointment.ProtocolNumber, 
                    institutionId, 
                    null
                );
                
                appointment.StartAppointmentQueue(calendarEventResponse.HangoutLink, officerId, queueUtcOffset);
                _appointmentRepository.Update(appointment);

                Task.Run(() => _unitOfWork.CommitAsync()).Wait();
                appointment = _appointmentRepository.GetByIdAsync(appointmentId).Result;
            }
            else
            {
                throw new BadRequestException(AppointmentError.WrongServiceDesk);
            }

            var viewModel = _mapper.Map<AppointmentViewModel>(appointment);
            return viewModel;
        }

        public void CloseAppointmentQueue(Guid appointmentId, CloseAppointmentQueuePayload payload)
        {
            var officerId = _apiContext.SecurityContext.Account.Id;

            var appointment = _appointmentRepository.GetWithAppointmentQueueByIdAsync(appointmentId).Result;
            if (appointment.OfficerId != officerId)
            {
                throw new BadRequestException(AppointmentError.WrongOfficer);
            }

            var serviceDesk = _serviceDeskService.GetById(appointment.ServiceDeskId);
            TimeZoneInfo instanceTimezone = TimeZoneInfo.FindSystemTimeZoneById(serviceDesk.CalendarTimeZone);

            var queueUtcOffset = instanceTimezone.BaseUtcOffset;

            if (_serviceDeskOfficerService.IsOfficer(serviceDesk.Id, officerId))
            {
                appointment.CloseAppointmentQueue(payload.AppointmentStatusId, queueUtcOffset);

                _appointmentRepository.Update(appointment);

                Task.Run(() => _unitOfWork.CommitAsync()).Wait();
            }
            else
            {
                throw new BadRequestException(AppointmentError.WrongServiceDesk);
            }
        }

        public AppointmentViewModel Feedback(FeedbackPayload payload)
        {
            var appointment = _appointmentRepository.GetByIdAsync(payload.AppointmentId).Result;

            if (appointment == null)
            {
                throw new BadRequestException(AppointmentError.AppointmentDoesNotExist);
            }

            appointment.Feedback(payload.CustomerRate, payload.CustomerNote);
            _appointmentRepository.Update(appointment);

            var saved = Task.Run(() => _unitOfWork.CommitAsync()).Result;
            if (saved == false)
            {
                throw new BadRequestException(AppointmentError.InsertError);
            }

            return _mapper.Map<AppointmentViewModel>(appointment);
        }

        #region Private Methods

        private AppointmentViewModel InsertAppointmentInVirtualQueue(CreateAppointmentPayload payload)
        {
            ValidateQueuePayLoad(payload);

            while (_busyServiceDesks.Where(x => x == payload.ServiceDeskId).Any())
            {
                Task.Delay(100).Wait();
            }

            _busyServiceDesks.Add(payload.ServiceDeskId);

            var serviceDesk = _serviceDeskService.GetById(payload.ServiceDeskId);

            TimeZoneInfo instanceTimezone = TimeZoneInfo.FindSystemTimeZoneById(serviceDesk.CalendarTimeZone);

            var queueUtcOffset = instanceTimezone.BaseUtcOffset;

            var appointmentsInQueue = Task.Run(()=>_appointmentRepository.GetAppointmentsWaitingInQueue(payload.ServiceDeskId)).Result;

            var nextProtocol = GenerateProtocolNumber(payload.ServiceDeskId);

            var newAppointment = new Appointment(payload.ServiceDeskId, DateTime.UtcNow.AddTicks(queueUtcOffset.Ticks), nextProtocol, (appointmentsInQueue.Count+1));

            _appointmentRepository.Add(newAppointment);

            var saved = Task.Run(() => _unitOfWork.CommitAsync()).Result;

            _busyServiceDesks.Remove(payload.ServiceDeskId);

            if (saved == false)
            {
                throw new BadRequestException(AppointmentError.InsertError);
            }

            var viewModel = _mapper.Map<AppointmentViewModel>(newAppointment);
            return viewModel;
        }

        private AppointmentViewModel InsertAppointmentInScheduling(CreateAppointmentPayload payload, ServiceDeskViewModel serviceDeskViewModel)
        {
            ValidatSchedulePayLoad(payload);

            var officer = Task.Run(() => _serviceDeskOfficerrepository.GetAsync(x => x.ServiceDeskId == serviceDeskViewModel.Id, includeProperties: "OfficerHours"))
                .Result
                .FirstOrDefault();

            if(officer == null)
            {
                throw new Exception("Nao possui officer");
            }

            while (_busyServiceDesks.Where(x => x == payload.ServiceDeskId).Any())
            {
                Task.Delay(100).Wait();
            }

            _busyServiceDesks.Add(payload.ServiceDeskId);

            var scheduledAppointment = _appointmentRepository.GetAsync(x => x.ServiceDeskId == payload.ServiceDeskId 
                && x.Date == payload.Date).Result;

            if (scheduledAppointment.Any())
            {
                throw new BadRequestException(AppointmentError.AppointmentCuncurrentScheduledDate);
            }

            // generate the protocol number
            string protocolNumber = GenerateProtocolNumber(serviceDeskViewModel.Id);
            if (protocolNumber == null)
            {
                throw new Exception("Nao possui protocolNumber");
            }

            var officerHour = officer.OfficerHours.FirstOrDefault(x =>
                x.DayOfWeek == payload.Date.DayOfWeek
                && payload.Date.TimeOfDay >= x.StartTime && payload.Date.TimeOfDay <= x.EndTime
            );

            TimeZoneInfo instanceTimezone = TimeZoneInfo.FindSystemTimeZoneById(serviceDeskViewModel.CalendarTimeZone);
            var schedulingUtcOffset = instanceTimezone.BaseUtcOffset;

            var appointment = new Appointment(serviceDeskViewModel.Id, payload.Date, protocolNumber, note: payload.Note, officeId: officer.OfficerId);            
            appointment.AppointmentSchedule = new AppointmentSchedule(officerHour.InstitutionServiceLocationId, appointment.Id, DateTime.UtcNow.AddTicks(schedulingUtcOffset.Ticks));

            //google meet attendees
            var eventAttendees = new List<EventAttendeePayload>
            {
                new EventAttendeePayload { DisplayName = officer.Name, Email = officer.Email },
                new EventAttendeePayload { DisplayName = payload.Customer.Name, Email = payload.Customer.Email }
            };

            //generate the Google Meet Id
            CalendarEventViewModel calendarEventResponse = InvokeCreateEvent(
                serviceDeskViewModel.CalendarName, 
                serviceDeskViewModel.CalendarTimeZone, 
                appointment.ProtocolNumber, 
                serviceDeskViewModel.InstitutionId, 
                eventAttendees, 
                payload.Date, 
                payload.Date.AddMinutes(30)
            );

            if (calendarEventResponse == null)
            {
                throw new Exception("Nao possui calendarEventResponse");
            }

            appointment.AddMeetId(calendarEventResponse.HangoutLink);

            //add the customer to the appointment
            Customer customer = CheckExistingCustomer(payload);

            appointment.AddCustomer(customer);
            _appointmentRepository.Add(appointment);

            var saved = Task.Run(() => _unitOfWork.CommitAsync()).Result;

            _busyServiceDesks.Remove(payload.ServiceDeskId);

            if (saved == false)
            {
                throw new BadRequestException(AppointmentError.InsertError);
            }

            return _mapper.Map<AppointmentViewModel>(appointment);
        }

        private Customer CheckExistingCustomer(CreateAppointmentPayload payload)
        {
            var existingCustomer = Task.Run(() => _customerRepository.GetByEmailAsync(payload.Customer.Email)).Result;

            if (existingCustomer != null)
            {
                existingCustomer.UpdateDetails(payload.Customer.Name, payload.Customer.PhoneNumber, payload.Customer.Address);
                _customerRepository.Update(existingCustomer);
                return existingCustomer;
            }

            //if not find existing customer with this email then we add a new customer
            var newCustomer = new Customer(payload.Customer.Name, payload.Customer.Email, payload.Customer.PhoneNumber, payload.Customer.Address);
            _customerRepository.Add(newCustomer);
            return newCustomer;
        }

        private void ValidateQueuePayLoad(CreateAppointmentPayload payload)
        {

        }

        private void ValidatSchedulePayLoad(CreateAppointmentPayload payload)
        {
            if (payload.Customer == null)
            {
                throw new Exception("Nao possui costumer");
            }

        }

        private void PagingValidation()
        {
            if (_apiContext.PagingContext.RequestPaging.Page <= 0)
            {
                _apiContext.PagingContext.RequestPaging.Page = 1;
            }

            if (_apiContext.PagingContext.RequestPaging.PageSize <= 0)
            {
                _apiContext.PagingContext.RequestPaging.PageSize = 20;
            }

            if (_apiContext.PagingContext.RequestPaging.PageSize > 100)
            {
                throw new BadRequestException(AppointmentError.PageMaximumExceeded);
            }
        }

        private string GenerateProtocolNumber(Guid serviceDeskId)
        {
            string nextProtocolNumber = "1"; //protocol number start from 1

            var lastAppointment = _appointmentRepository.GetLastByServiceDeskIdAsync(serviceDeskId).Result;
            if (lastAppointment != null)
            {
                var lastProtocol = int.Parse(lastAppointment.ProtocolNumber);
                nextProtocolNumber = (lastProtocol + 1).ToString();
            }

            return nextProtocolNumber;
        }

        private CalendarEventViewModel InvokeCreateEvent(string calendarName, string calendarTimeZone, string protocolNumber, Guid institutionId, IList<EventAttendeePayload> eventAttendees, DateTime? startTime = null, DateTime? endTime = null)
        {
            CreateCalendarEventPayload createEventPayload = new()
            {
                EventSubject = $"Agendamento - Protocolo Número #{protocolNumber}",
                CalendarName = calendarName,
                CalendarTimeZone = calendarTimeZone,
                ProtocolNumber = protocolNumber,
                InstitutionId = institutionId,
                StartTime = startTime,
                EndTime = endTime,
                EventAttendees = eventAttendees ?? new List<EventAttendeePayload>()
            };

            var calendarEventResponse = _calendarService.CreateCalendarEvent(createEventPayload);
            return calendarEventResponse;
        }

        #endregion

    }
}

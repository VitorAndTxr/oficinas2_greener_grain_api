using API.Framework.Interfaces;
using AutoMapper;
using GreenerGrain.Framework.Exceptions;
using GreenerGrain.Framework.Services;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.Enumerators;
using GreenerGrain.Domain.Payloads;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Provider.Providers;
using GreenerGrain.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GreenerGrain.Service.Services
{
    public class CalendarService : ServiceBase, ICalendarService
    {
        #region Fields

        private readonly IApiContext _apiContext;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly IInstitutionProviderSettingsRepository _institutionProviderSettingsRepository;

        #endregion

        #region Constructor

        public CalendarService(
            IApiContext apiContext
            , IConfiguration configuration
            , IServiceProvider serviceProvider
            , IInstitutionProviderSettingsRepository institutionProviderSettingsRepository            
            , IMapper mapper) : base(apiContext)
        {
            _apiContext = apiContext;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _institutionProviderSettingsRepository = institutionProviderSettingsRepository;            
            _mapper = mapper;
        }

        #endregion

        public CalendarEventViewModel CreateCalendarEvent(CreateCalendarEventPayload payload)
        {
            ValidatePayLoad(payload);

            var calendarProvider = GetCalendarProvider(payload.InstitutionId);

            var createdEvent = calendarProvider.CreateEvent(payload);

            if (createdEvent != null)
            {
                return _mapper.Map<CalendarEventViewModel>(createdEvent);
            }

            return null;
        }

        #region Private Methods

        private void ValidatePayLoad(CreateCalendarEventPayload payload)
        {
            if (string.IsNullOrEmpty(payload.CalendarName))
            {
                _apiContext.Errors.Add(new GreenerGrain.Framework.Result.Error(AppointmentError.NotFoundCalendarName));
            }

            if (payload.InstitutionId == Guid.Empty)
            {
                _apiContext.Errors.Add(new GreenerGrain.Framework.Result.Error(AppointmentError.NotFoundInstitutionId));
            }

            if (_apiContext.Errors.Count > 0)
            {
                throw new BadRequestException(AppointmentError.InvalidPayload);
            }
        }

        private CalendarProvider GetCalendarProvider(Guid institutionId)
        {
            var institutionSettings = Task.Run(() => _institutionProviderSettingsRepository.GetAsync(x => x.InstitutionId == institutionId)).Result;
            return new CalendarProvider(_serviceProvider, institutionSettings.FirstOrDefault(), _configuration);
        }

        #endregion

    }

}

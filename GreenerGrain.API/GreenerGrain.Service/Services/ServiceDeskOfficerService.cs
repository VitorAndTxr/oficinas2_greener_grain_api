using API.Framework.Interfaces;
using AutoMapper;
using GreenerGrain.Framework.Exceptions;
using GreenerGrain.Framework.Services;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.Enumerators;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Service.Interfaces;
using System;
using System.Linq;

namespace GreenerGrain.Service.Services
{
    public class ServiceDeskOfficerService : ServiceBase, IServiceDeskOfficerService
    {
        private readonly IApiContext _apiContext;
        private readonly IMapper _mapper;
        private readonly IServiceDeskRepository _serviceDeskRepository;
        private readonly IServiceDeskOfficerRepository _serviceDeskOfficerRepository;

        public ServiceDeskOfficerService(
            IApiContext apiContext
            , IMapper mapper
            , IServiceDeskOfficerRepository serviceDeskOfficerRepository
            , IServiceDeskRepository serviceDeskRepository) : base(apiContext)
        {
            _apiContext = apiContext;
            _mapper = mapper;
            _serviceDeskOfficerRepository = serviceDeskOfficerRepository;
            _serviceDeskRepository = serviceDeskRepository;
        }

        public OfficerQueueViewModel GetServiceDeskOfficer()
        {
            var officerId = _apiContext.SecurityContext.Account.Id;
            var serviceDeskOfficer = _serviceDeskOfficerRepository.GetByOfficerIdAsync(officerId).Result;

            if (serviceDeskOfficer == null)
            {
                throw new BadRequestException(OfficerError.OfficerDoesNotExist);
            }

            OfficerQueueViewModel result = new();

            var queueService = serviceDeskOfficer.Where(x => x.ServiceDesk.ServiceDeskTypeId == ServiceDeskTypeEnum.VirtualQueue).FirstOrDefault();
            if (queueService != null)
            {
                result.VirtualQueue = _mapper.Map<ServiceDeskViewModel>(queueService.ServiceDesk);
            }

            var schedulingService = serviceDeskOfficer.Where(x => x.ServiceDesk.ServiceDeskTypeId == ServiceDeskTypeEnum.ScheduleAppointment).FirstOrDefault();
            if (schedulingService != null)
            {
                result.VirtualScheduling = _mapper.Map<ServiceDeskViewModel>(schedulingService.ServiceDesk);
            }

            return result;
        }

        public bool IsOfficer(Guid serviceDeskId, Guid officerId)
        {
            var result = _serviceDeskOfficerRepository
                .GetOfficerByIdAndServiceDeskAsync(officerId, serviceDeskId).Result;

            return result != null;
        }

    }
}

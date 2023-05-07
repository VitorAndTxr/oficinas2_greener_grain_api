using API.Framework.Interfaces;
using AutoMapper;
using GreenerGrain.Framework.Database.EfCore.Interface;
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
    public class OfficerHourService : ServiceBase, IOfficerHourService
    {
        #region Fields

        private readonly IApiContext _apiContext;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOfficerHourRepository _officerHourRepository;
        private readonly IServiceDeskOfficerRepository _serviceDeskOfficerRepository;
        
        #endregion

        #region Constructor

        public OfficerHourService(
            IApiContext apiContext
            , IMapper mapper
            , IUnitOfWork unitOfWork
            , IOfficerHourRepository officerHourRepository
            , IServiceDeskOfficerRepository serviceDeskOfficerRepository) : base(apiContext)
        {
            _apiContext = apiContext;
            _mapper = mapper;
            _unitOfWork = unitOfWork;

            _officerHourRepository = officerHourRepository;
            _serviceDeskOfficerRepository = serviceDeskOfficerRepository;
        }

        #endregion

        public IList<OfficerHoursViewModel> GetOfficerHours()
        {
            var officerId = _apiContext.SecurityContext.Account.Id;

            var serviceDesk = _serviceDeskOfficerRepository
                .GetAsync(x => x.OfficerId == officerId && x.ServiceDesk.ServiceDeskTypeId == ServiceDeskTypeEnum.ScheduleAppointment)
                .Result
                .FirstOrDefault();

            if (serviceDesk != null)
            {
                var listOfOfficerHours = _officerHourRepository
                    .GetAsync(x => x.ServiceDeskOfficerId == serviceDesk.Id)
                    .Result;

                return _mapper.Map<IList<OfficerHoursViewModel>>(listOfOfficerHours);
            }

            return null;
        }

        public IList<OfficerHoursViewModel> GetOfficerHoursByServiceDeskOfficerId(Guid serviceDeskOfficerId)
        {

            var listOfOfficerHours = _officerHourRepository
                .GetAsync(x => x.ServiceDeskOfficerId == serviceDeskOfficerId)
                .Result;

            if(listOfOfficerHours != null)
            {
                return _mapper.Map<IList<OfficerHoursViewModel>>(listOfOfficerHours);
            }

            return null;
        }

        public bool SaveOfficerHours(SaveOfficerHoursPayload payload)
        {
            var officerId = _apiContext.SecurityContext.Account.Id;

            var serviceDeskOfficer = _serviceDeskOfficerRepository.GetAsync(x => x.ServiceDeskId == payload.ServiceDeskId && x.OfficerId == officerId).Result.FirstOrDefault();

            if (serviceDeskOfficer != null)
            {

                foreach (var hour in payload.Hours)
                {
                    TimeSpan.TryParse(hour.StartTime, out TimeSpan startTime);
                    TimeSpan.TryParse(hour.EndTime, out TimeSpan endTime);


                    var existingOfficerHour = Task.Run(() => _officerHourRepository.GetAsync(x => x.Id == hour.Id)).Result
                        .FirstOrDefault();

                    if (existingOfficerHour != null)
                    {
                        existingOfficerHour.Edit(hour.InstitutionServiceLocationId, hour.DayOfWeek, startTime, endTime);
                        _officerHourRepository.Update(existingOfficerHour);
                    }
                    else
                    {
                        var newOfficerHour = new OfficerHour(serviceDeskOfficer.Id, hour.InstitutionServiceLocationId, hour.DayOfWeek, startTime, endTime);
                        _officerHourRepository.Add(newOfficerHour);
                    }
                }
            
                OnSaveDeleteOfficerHours(payload, serviceDeskOfficer.Id);
            }
            else
            {
                throw new Exception();
            }

            var success = Task.Run(() => _unitOfWork.CommitAsync()).Result;
            return success;
        }

        private void OnSaveDeleteOfficerHours(SaveOfficerHoursPayload payload, Guid serviceDeskOfficerId)
        {
            var payloadIds = payload.Hours.Select(x => x.Id).ToArray();
            
            var hoursToDelete = Task.Run(() => _officerHourRepository
                .GetAsync(x => !payloadIds.Contains(x.Id) && x.ServiceDeskOfficerId == serviceDeskOfficerId))
                .Result;

            _officerHourRepository.DeleteRange(hoursToDelete.ToList());
        }
    }
}

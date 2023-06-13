using API.Framework.Interfaces;
using AutoMapper;
using GreenerGrain.Framework.Services;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Service.Interfaces;
using System.Collections.Generic;
using GreenerGrain.Domain.Entities;
using GreenerGrain.Domain.Payloads;
using GreenerGrain.Data.Repositories;
using System.Threading.Tasks;
using GreenerGrain.Framework.Database.EfCore.Interface;
using System;

namespace GreenerGrain.Service.Services
{
    public class UnitService : ServiceBase, IUnitService
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UnitService(
            IApiContext apiContext,
            IUnitOfWork unitOfWork
            , IUnitRepository unitRepository
            , IMapper mapper)
                : base(apiContext)
        {
            _unitOfWork = unitOfWork;
            _unitRepository = unitRepository;
            _mapper = mapper;
        }
        public UnitViewModel GetById(Guid id)
        {
            var unit = _unitRepository.GetByIdAsync(id).Result;

            var unitViewModel = _mapper.Map<UnitViewModel>(unit);
            return unitViewModel;
        }



        public UnitViewModel GetByModuleId(Guid moduleId)
        {
            var unit = _unitRepository.GetByModuleIdAsync(moduleId).Result;

            var unitViewModel = _mapper.Map<UnitViewModel>(unit);
            return unitViewModel;
        }


        public UnitViewModel GetByUnitCode(string unitCode)
        {
            var unit = _unitRepository.GetByUnitCode(unitCode).Result;

            var unitViewModel = _mapper.Map<UnitViewModel>(unit);
            return unitViewModel;
        }

        public void VerifyStatus()
        {
            var unitList = _unitRepository.ListUnitGettingOffline().Result;

            foreach(var unit in unitList)
            {
                unit.isOffline();
            }
            _unitRepository.UpdateRange(unitList);

            var result = Task.Run(() => _unitOfWork.CommitAsync()).Result;
        }

        public bool SetUnitBusy(Guid id)
        {
            var unit = _unitRepository.GetByIdAsync(id).Result;


            unit.isBusy();

            _unitRepository.Update(unit);

            var result = Task.Run(() => _unitOfWork.CommitAsync()).Result;

            return result;
        }

        public bool UnitAlive(UnitAlivePayload payload)
        {
            var unit = _unitRepository.GetByIdAsync(payload.Id).Result;

            unit.isAlive(payload.Ip, payload.Status);

            _unitRepository.Update(unit);

            var result = Task.Run(() => _unitOfWork.CommitAsync()).Result;

            return result;

        }


    }
}

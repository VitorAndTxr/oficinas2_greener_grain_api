using GreenerGrain.Domain.Payloads;
using GreenerGrain.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace GreenerGrain.Service.Interfaces
{
    public interface IUnitService
    {
        UnitViewModel GetByUnitCode(string unitCode);
        UnitViewModel GetById(Guid id);

        UnitViewModel GetByModuleId(Guid id);
        bool SetUnitBusy(Guid id);

        void VerifyStatus();
        bool UnitAlive(UnitAlivePayload payload);

    }
}
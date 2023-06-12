using GreenerGrain.Domain.Payloads;
using GreenerGrain.Domain.ViewModels;
using System.Collections.Generic;

namespace GreenerGrain.Service.Interfaces
{
    public interface IUnitService
    {
        UnitViewModel GetByUnitCode(string unitCode);
        void VerifyStatus();
        bool UnitAlive(UnitAlivePayload payload);

    }
}
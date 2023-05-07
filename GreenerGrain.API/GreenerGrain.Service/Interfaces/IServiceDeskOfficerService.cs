 using GreenerGrain.Domain.ViewModels;
using System;

namespace GreenerGrain.Service.Interfaces
{
    public interface IServiceDeskOfficerService
    {
        OfficerQueueViewModel GetServiceDeskOfficer();

        bool IsOfficer(Guid ServiceAttendanceId, Guid attendantId);
    }    
}

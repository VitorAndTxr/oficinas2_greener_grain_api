using GreenerGrain.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace GreenerGrain.Service.Interfaces
{
    public interface IServiceDeskService
    {
        ServiceDeskViewModel GetById(Guid id);
        ServiceDeskViewModel GetByCode(string code);
        IList<ServiceDeskViewModel> ListScheduleServices(Guid institutionId);
        List<ServiceDeskOfficerAvaliabilityViewModel> GetServiceDeskSchedulingAvailability(Guid serviceDeskId);
        WaitingTimeViewModel GetQueueAverageWaitingTime(Guid serviceDeskId);

    }
}

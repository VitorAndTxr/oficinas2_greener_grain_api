using GreenerGrain.Domain.Entities;
using GreenerGrain.Domain.Payloads;
using GreenerGrain.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace GreenerGrain.Service.Interfaces
{
    public interface IOfficerHourService
    {
        IList<OfficerHoursViewModel> GetOfficerHours();

        IList<OfficerHoursViewModel> GetOfficerHoursByServiceDeskOfficerId(Guid serviceDeskOfficerId);

        bool SaveOfficerHours(SaveOfficerHoursPayload payload);
    }

}

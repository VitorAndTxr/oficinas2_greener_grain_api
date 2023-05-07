using GreenerGrain.Framework.Database.EfCore.Interface;
using GreenerGrain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenerGrain.Data.Interfaces
{
    public interface IAppointmentRepository : IRepositoryBase<Appointment>
    {
        Task<List<Appointment>> GetAllByServiceDeskIdAsync(Guid serviceDeskId);
        Task<Appointment> GetLastByServiceDeskIdAsync(Guid serviceDeskId);
        Task<Appointment> GetWithAppointmentQueueByIdAsync(Guid id);
        Task<List<Appointment>> GetAppointmentsWaitingInQueue(Guid serviceDeskId);
        Task<string> GetLastAppointmentProtocol(Guid serviceDeskId);
        Task<List<Appointment>> GetLastFiveAttendedByServiceDeskId(Guid serviceDeskId);

    }
}

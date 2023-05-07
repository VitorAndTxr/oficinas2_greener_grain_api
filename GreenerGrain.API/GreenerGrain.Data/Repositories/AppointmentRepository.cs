using API.Framework.Interfaces;
using GreenerGrain.Framework.Database.EfCore.Factory;
using GreenerGrain.Framework.Database.EfCore.Repository;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.Entities;
using GreenerGrain.Domain.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenerGrain.Data.Repositories
{
    public class AppointmentRepository : RepositoryBase<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(IDbFactory dbFactory, IApiContext apiContext)
                    : base(dbFactory, apiContext)
        {
        }

        public async Task<List<Appointment>> GetAllByServiceDeskIdAsync(Guid serviceDeskId)
        {
            var result = await GetAsync(x => x.ServiceDeskId == serviceDeskId);

            return result.ToList();
        }

        public async Task<List<Appointment>> GetLastFiveAttendedByServiceDeskId(Guid serviceDeskId)
        {
            var result = await GetAsync(x => 
                x.ServiceDeskId == serviceDeskId && x.AppointmentStatusId != AppointmentStatusEnum.New
                    && x.ServiceDesk.ServiceDeskTypeId == ServiceDeskTypeEnum.VirtualQueue,
                x => x.OrderByDescending(x=> x.CreationDate),
                "AppointmentQueue"
            );

            return result.Take(5).ToList();
        }

        public async Task<Appointment> GetLastByServiceDeskIdAsync(Guid serviceDeskId)
        {
            var result = await GetAsync(x => x.ServiceDeskId == serviceDeskId);

            return result.OrderByDescending(X => X.CreationDate).FirstOrDefault();
        }

        public async Task<Appointment> GetWithAppointmentQueueByIdAsync(Guid id)
        {
            var result = await GetAsync(x => x.Id == id, null, "AppointmentQueue");

            return result.FirstOrDefault();
        }


        public async Task<Appointment> GetTheLastAppoint(Guid id)
        {
            var result = await GetAsync(x => x.Id == id, null, "AppointmentQueue");

            return result.FirstOrDefault();
        }
        public async Task<List<Appointment>> GetAppointmentsWaitingInQueue(Guid serviceDeskId)
        {
            var result = await GetAsync(x => x.ServiceDeskId == serviceDeskId && x.AppointmentStatusId == AppointmentStatusEnum.New);

            return result.OrderByDescending(x => x.Date).ToList();
        }

        public async Task<string> GetLastAppointmentProtocol(Guid serviceDeskId)
        {
            var result = await GetAsync(x => x.ServiceDeskId == serviceDeskId);
            if (result.Count() > 0)
            {

                return result.OrderByDescending(x => x.Date).FirstOrDefault().ProtocolNumber;
            }

            return "0";

        }


    }
}
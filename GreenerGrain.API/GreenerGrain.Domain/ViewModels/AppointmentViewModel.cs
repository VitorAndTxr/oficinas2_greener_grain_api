using GreenerGrain.Framework.Database.EfCore.Model;
using GreenerGrain.Domain.Entities;
using GreenerGrain.Domain.Enumerators;
using System;

namespace GreenerGrain.Domain.ViewModels
{
    public class AppointmentViewModel
    {
        public Guid Id { get; set; }
        public Guid ServiceDeskId { get; private set; }
        public Guid? CustomerId { get; private set; }
        public Guid OfficerId { get; set; }
        public DateTime Date { get; private set; }
        public AppointmentStatusEnum AppointmentStatus { get; set; }

        public string ProtocolNumber { get; private set; }
        public string MeetId { get; set; }
        public string Note { get; set; }

        public virtual ServiceDeskViewModel ServiceDesk { get; set; }
        public virtual CustomerViewModel Customer { get; set; }
        public virtual AppointmentQueueViewModel AppointmentQueue { get; set; }

    }
}

using GreenerGrain.Framework.Database.EfCore.Model;
using GreenerGrain.Domain.Enumerators;
using System;

namespace GreenerGrain.Domain.Entities
{
    public class Appointment : AuditEntity<Guid>
    {
        public Guid ServiceDeskId { get; private set; }        
        public Guid? CustomerId { get; private set; }        
        public Guid? OfficerId { get; set; }
        public AppointmentStatusEnum AppointmentStatusId { get; private set; }
        public DateTime Date { get; private set; }        
        public string ProtocolNumber { get; private set; }        
        public string MeetId { get; private set; }
        public string Note { get; private set; }
        public int? CustomerRate { get; set; }
        public string CustomerNote { get; set; }

        public virtual ServiceDesk ServiceDesk { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual AppointmentQueue AppointmentQueue { get; set; }
        public virtual AppointmentSchedule AppointmentSchedule { get; set; }

        protected Appointment() { }
     
        public Appointment(Guid serviceDeskId, DateTime date, string protocolNumber, string meetId = "", string note = "", Guid? customerId = null, Guid? officeId = null)
        {
            SetId(Guid.NewGuid());
            Activate();

            ServiceDeskId = serviceDeskId;
            CustomerId = customerId;
            OfficerId = officeId;
            Date = date;
            ProtocolNumber = protocolNumber;
            MeetId = meetId;
            Note = note;
            AppointmentStatusId = AppointmentStatusEnum.New;
        }

        public Appointment(Guid serviceDeskId, DateTime date, string protocolNumber, int startPosition, string meetId = "", string note = "", Guid? customerId = null, Guid? officeId = null)
        {
            SetId(Guid.NewGuid());
            Activate();

            ServiceDeskId = serviceDeskId;
            CustomerId = customerId;
            OfficerId = officeId;
            Date = date;
            ProtocolNumber = protocolNumber;
            MeetId = meetId;
            Note = note;
            AppointmentStatusId = AppointmentStatusEnum.New;

            AppointmentQueue = new AppointmentQueue(Id, date, startPosition);
        }

        public void SetStatus(AppointmentStatusEnum status)
        {
            this.AppointmentStatusId = status;
        }

        public void AddCustomer(Customer customer)
        {
            Customer = customer;
            CustomerId = customer.Id;
        }

        public void AddMeetId(string meetId)
        {
            this.MeetId = meetId;
        }

        public void StartAppointmentQueue(string meetId, Guid officerId, TimeSpan queueOffset)
        {
            this.MeetId = meetId;
            this.OfficerId = officerId;
            AppointmentStatusId = AppointmentStatusEnum.InProgress;
            AppointmentQueue.SetQueueAttendHour(DateTime.UtcNow.AddTicks(queueOffset.Ticks));
        }

        public void CloseAppointmentQueue(AppointmentStatusEnum status, TimeSpan queueOffset)
        {
            this.AppointmentStatusId = status;
            AppointmentQueue.SetAttendCloseHour(DateTime.UtcNow.AddTicks(queueOffset.Ticks));
        }

        public void Feedback(int customerRate, string customerNote)
        {
            this.CustomerRate = customerRate;
            this.CustomerNote = customerNote;
        }
    }
}

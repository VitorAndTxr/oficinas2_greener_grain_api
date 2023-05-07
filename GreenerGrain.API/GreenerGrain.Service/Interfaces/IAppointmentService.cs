using GreenerGrain.Domain.Payloads;
using GreenerGrain.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace GreenerGrain.Service.Interfaces
{
    public interface IAppointmentService
    {
        IList<AppointmentViewModel> ListByQueueId(AppointmentListPayload payload);
        //AppointmentViewModel GetById(Guid id);
        AppointmentViewModel StartAppointmentQueue(Guid appointmentId);
        AppointmentViewModel CreateAppointment(CreateAppointmentPayload payload);
        void CloseAppointmentQueue(Guid appointmentId, CloseAppointmentQueuePayload payload);        
        AppointmentViewModel Feedback(FeedbackPayload payload);
    }
}

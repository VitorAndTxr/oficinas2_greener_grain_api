using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenerGrain.Domain.Payloads
{
    public class SendEmailPayload
    {
        public string EmailTo { get; set; }
        public string NameTo { get; set; }
        public string AppointmentLink { get; set; }
    }
}

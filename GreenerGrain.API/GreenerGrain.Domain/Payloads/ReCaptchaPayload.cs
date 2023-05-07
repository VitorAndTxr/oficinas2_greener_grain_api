using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenerGrain.Domain.Payloads
{
    public class ReCaptchaPayload
    {
        [Required]
        public string Token { get; set; }
    }
}

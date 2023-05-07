using GreenerGrain.Domain.Payloads;
using GreenerGrain.Domain.ViewModels;

namespace GreenerGrain.Service.Interfaces
{
    public interface IRecaptchaService 
    {
        ReCaptchaViewModel Verify(ReCaptchaPayload payload);
    }
}

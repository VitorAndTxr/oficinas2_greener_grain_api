using GreenerGrain.Framework.Security;
using GreenerGrain.Domain.Entities;
using GreenerGrain.Domain.ViewModels;

namespace GreenerGrain.Service.AutoMapper
{
    public class DomainToViewModelMappingProfile : global::AutoMapper.Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<GreenerGrain.Domain.Entities.Profile, ProfileViewModel>();
        }
    }
}

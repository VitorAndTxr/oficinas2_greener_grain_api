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
            CreateMap<GreenerGrain.Domain.Entities.Unit, UnitViewModel>();
            CreateMap<GreenerGrain.Domain.Entities.Module, ModuleViewModel>();
            CreateMap<GreenerGrain.Domain.Entities.Grain, GrainViewModel>();
            CreateMap<GreenerGrain.Domain.Entities.Account, AccountViewModel>();
            CreateMap<GreenerGrain.Domain.Entities.AccountWallet, AccountWalletViewModel>();
            CreateMap<GreenerGrain.Domain.Entities.BuyTransaction, BuyTransactionViewModel>();




        }
    }
}

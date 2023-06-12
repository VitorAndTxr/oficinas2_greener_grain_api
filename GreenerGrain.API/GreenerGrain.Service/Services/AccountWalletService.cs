using API.Framework.Interfaces;
using AutoMapper;
using GreenerGrain.Framework.Services;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Service.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GreenerGrain.Service.Services
{
    public class AccountWalletService : ServiceBase, IAccountWalletService
    {
        private readonly IApiContext _apiContext;

        private readonly IAccountWalletRepository _accountWalletRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public AccountWalletService(
            IApiContext apiContext
            , IConfiguration configuration
            , IAccountWalletRepository accountWalletRepository
            , IMapper mapper)
                : base(apiContext)
        {
            _apiContext= apiContext;
            _configuration = configuration;
            _accountWalletRepository = accountWalletRepository;
            _mapper = mapper;
        }

        public AccountWalletViewModel GetByUserWallet()
        {
            var userId = _apiContext.SecurityContext.Account.Id;
            var accountWallet = _accountWalletRepository.GetByAccountId(userId).Result;

            var accountWalletViewModel = _mapper.Map<AccountWalletViewModel>(accountWallet);
            return accountWalletViewModel;
        }
    }
}

using API.Framework.Interfaces;
using AutoMapper;
using GreenerGrain.Framework.Services;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using GreenerGrain.Framework.Database.EfCore.Interface;
using GreenerGrain.Data.Repositories;
using System.Threading.Tasks;

namespace GreenerGrain.Service.Services
{
    public class AccountWalletService : ServiceBase, IAccountWalletService
    {
        private readonly IApiContext _apiContext;

        private readonly IAccountWalletRepository _accountWalletRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;


        public AccountWalletService(
            IApiContext apiContext
            , IUnitOfWork unitOfWork
            , IConfiguration configuration
            , IAccountWalletRepository accountWalletRepository
            , IMapper mapper)
                : base(apiContext)
        {
            _apiContext= apiContext;
            _configuration = configuration;
            _accountWalletRepository = accountWalletRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public AccountWalletViewModel GetByUserWallet()
        {
            var userId = _apiContext.SecurityContext.Account.Id;
            var accountWallet = _accountWalletRepository.GetByAccountId(userId).Result;

            var accountWalletViewModel = _mapper.Map<AccountWalletViewModel>(accountWallet);
            return accountWalletViewModel;
        }

        public bool RemoveUserCredts(float value)
        {
            var userId = _apiContext.SecurityContext.Account.Id;
            var accountWallet = _accountWalletRepository.GetByAccountId(userId).Result;

            accountWallet.RemoveCredits(value);
            _accountWalletRepository.Update(accountWallet);

            var result = Task.Run(() => _unitOfWork.CommitAsync()).Result;

            return result;
        }
    }
}

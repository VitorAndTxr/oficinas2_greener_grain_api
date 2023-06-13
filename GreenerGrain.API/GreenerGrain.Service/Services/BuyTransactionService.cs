using API.Framework.Interfaces;
using AutoMapper;
using GreenerGrain.Framework.Services;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Service.Interfaces;
using GreenerGrain.Domain.Payloads;
using GreenerGrain.Framework.Database.EfCore.Interface;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using GreenerGrain.Domain.Entities;
using System.Threading.Tasks;

namespace GreenerGrain.Service.Services
{
    public class BuyTransactionService : ServiceBase, IBuyTransactionService
    {
        private readonly IBuyTransactionRepository _buyTransactionRepository;
        private readonly IUnitService _unitService;
        private readonly IAccountWalletService _accountWalletService;


        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public BuyTransactionService(
            IApiContext apiContext
            , IUnitOfWork unitOfWork
            , IBuyTransactionRepository buyTransactionRepository

            , IAccountWalletService accountWalletService
            , IUnitService unitService
            , IMapper mapper)
                : base(apiContext)
        {
            _accountWalletService = accountWalletService;
            _unitOfWork = unitOfWork;
            _buyTransactionRepository = buyTransactionRepository;
            _unitService= unitService;
            _mapper = mapper;
        }

        public BuyTransactionViewModel CreateTransaction(CreateBuyTransactionPayload payload)
        {
            var userWallet = _accountWalletService.GetByUserWallet();

            BuyTransactionViewModel result = null;

            if (userWallet == null)
            {
                throw new System.Exception("Usuário nao possui carteira");
            }

            var unit = _unitService.GetByModuleId(payload.ModuleId);

            var module = unit.Modules.Where(x =>x.Id == payload.ModuleId).FirstOrDefault();

            if (unit.State == Domain.Enumerators.UnitStateEnum.Offline)
            {
                throw new System.Exception("Unidade Offline");
            }

            if (unit.State == Domain.Enumerators.UnitStateEnum.Busy)
            {
                throw new System.Exception("Unidade ocupada");
            }

            //Validar quantidade de grãos

            var totalValue = (module.Grain.Price * payload.Quantity) / 1000;

            if(totalValue > userWallet.Credits)
            {
                throw new System.Exception("Créditos Insuficientes");
            }


            using(HttpClient client = new HttpClient())
            {

                var requestGrainPayload = new RequestGrainPayload() { 
                    peso = payload.Quantity,
                    module = module.Order
                };

                var json = JsonConvert.SerializeObject(requestGrainPayload);
                var url = "http://" + unit.Ip + "/requestGrain";

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using HttpResponseMessage response = client.PostAsync(url, data).Result;
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    _unitService.SetUnitBusy(unit.Id);
                    _accountWalletService.RemoveUserCredts(totalValue);

                    var newTransaction = new BuyTransactionViewModel()
                    {
                        Quantity = payload.Quantity,
                        Value = totalValue,
                        BuyerId = userWallet.AccountId,
                        GrainId = payload.GrainId,
                        ModuleId = module.Id,
                    };

                    result = AddTransaction(newTransaction);
                }
                string responseBody = response.Content.ReadAsStringAsync().Result;
            }

            return result;
        }

        public BuyTransactionViewModel AddTransaction(BuyTransactionViewModel payload)
        {
            BuyTransactionViewModel result = null;
            var newTransaction = new BuyTransaction(payload.Quantity, payload.Value, payload.BuyerId, payload.ModuleId, payload.GrainId);

            _buyTransactionRepository.Add(newTransaction);

            var commit = Task.Run(() => _unitOfWork.CommitAsync()).Result;

            if(commit == true)
            {
                result = _mapper.Map<BuyTransactionViewModel>(_buyTransactionRepository.GetByIdAsync(newTransaction.Id).Result);
            }

            return result;

        }
    }
}

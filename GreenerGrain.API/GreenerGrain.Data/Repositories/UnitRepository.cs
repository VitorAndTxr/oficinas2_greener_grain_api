using API.Framework.Interfaces;
using GreenerGrain.Framework.Database.EfCore.Factory;
using GreenerGrain.Framework.Database.EfCore.Repository;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.Entities;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;

namespace GreenerGrain.Data.Repositories
{
    public class UnitRepository : RepositoryBase<Unit>, IUnitRepository
    {
        public UnitRepository(IDbFactory dbFactory, IApiContext apiContext)
                   : base(dbFactory, apiContext)
        {
        }

        public async Task<Unit> GetByUnitCode(string code)
        {
            var result = await GetAsync(x => x.Code == code,
                includeProperties: "Modules.Grain");

            return result.FirstOrDefault();
        }

        public async Task<Unit> GetByIdAsync(string id)
        {
            var result = await GetAsync(x => x.Id == Guid.Parse(id),
                includeProperties: "Modules.Grain");

            return result.FirstOrDefault();
        }

        public async Task<List<Unit>> ListUnitGettingOffline()
        {
            var result = await GetAsync(
                x =>
                (x.State == Domain.Enumerators.UnitStateEnum.Idle ||
                x.State == Domain.Enumerators.UnitStateEnum.Busy)
                && (x.UpdateDate < DateTime.Now.AddMinutes(-5)||
                x.UpdateDate == null
                )
                ,includeProperties: "Modules.Grain");

            return result.ToList();
        }

        public async Task<List<Unit>> ListAll()
        {
            var result = await GetAsync(null,
                includeProperties: "Modules.Grain");

            return result.ToList();
        }
    }
}
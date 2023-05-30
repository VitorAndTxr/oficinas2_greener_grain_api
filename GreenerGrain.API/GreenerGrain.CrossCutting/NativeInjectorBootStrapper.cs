using GreenerGrain.Framework.Database.EfCore.Context;
using GreenerGrain.Framework.Database.EfCore.Factory;
using GreenerGrain.Framework.Database.EfCore.Interface;
using GreenerGrain.Framework.Database.EfCore.Repository;
using GreenerGrain.Data.Context;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Data.Repositories;
using GreenerGrain.Service.Interfaces;
using GreenerGrain.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GreenerGrain.CrossCutting
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IApiClient, ApiClient>();
            AddServices(services);
            AddDatabase(services);
            AddRepositories(services);
        }

        public static void AddServices(IServiceCollection services)
        {


            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IAccountProfileService, AccountProfileService>();


        }

        public static void AddDatabase(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDatabaseContext, DatabaseContext>();
            services.AddScoped<Func<IDatabaseContext>>((provider) => () => provider.GetService<DatabaseContext>());
            services.AddScoped<IDbFactory, DbFactory>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            services.AddScoped<IAccountProfileRepository, AccountProfileRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();

        }

    }
}


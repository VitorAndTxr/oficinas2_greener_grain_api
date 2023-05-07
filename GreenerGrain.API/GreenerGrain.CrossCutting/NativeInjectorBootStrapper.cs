using GreenerGrain.Framework.Database.EfCore.Context;
using GreenerGrain.Framework.Database.EfCore.Factory;
using GreenerGrain.Framework.Database.EfCore.Interface;
using GreenerGrain.Framework.Database.EfCore.Repository;
using GreenerGrain.Data.Context;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Data.Repositories;
using GreenerGrain.Provider.Implementations.Google;
using GreenerGrain.Provider.Interfaces.Google;
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
            services.AddSingleton<IGoogleCredentialBuilder, GoogleClientBuilder>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IApiClient, ApiClient>();
            AddServices(services);
            AddDatabase(services);
            AddRepositories(services);
        }

        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped<ICalendarService, CalendarService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IInstitutionServiceLocationService, InstitutionServiceLocationService>();
            services.AddScoped<IServiceDeskService, ServiceDeskService>();
            services.AddScoped<IServiceDeskOfficerService, ServiceDeskOfficerService>();
            services.AddScoped<IOfficerHourService, OfficerHourService>();
            services.AddScoped<IRecaptchaService, RecaptchaService>();
            services.AddScoped<IReportService, ReportService>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IProviderService , ProviderService>();
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
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IAppointmentQueueRepository, AppointmentQueueRepository>();
            services.AddScoped<IServiceDeskRepository, ServiceDeskRepository>();
            services.AddScoped<IServiceDeskOfficerRepository, ServiceDeskOfficerRepository>();
            services.AddScoped<IOfficerPauseRepository, OfficerPauseRepository>();
            services.AddScoped<IOfficerHourRepository, OfficerHourRepository>();
            services.AddScoped<IServiceDeskOpeningHoursRepository, ServiceDeskOpeningHoursRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IInstitutionServiceLocationRepository, InstitutionServiceLocationRepository>();
            services.AddScoped<IInstitutionProviderSettingsRepository, InstitutionProviderSettingsRepository>();
            services.AddScoped<IInstitutionReportRepository, InstitutionReportRepository>();

            services.AddScoped<IAccountProfileRepository, AccountProfileRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IInstitutionRepository, InstitutionRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();

        }

    }
}


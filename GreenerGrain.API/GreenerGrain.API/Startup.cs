using GreenerGrain.API.Config;
using GreenerGrain.Framework.StartupBase;
using GreenerGrain.Service.Interfaces;
using GreenerGrain.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GreenerGrain.API
{
    public class Startup : RestStartupBase
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddHttpContextAccessor();

            services.AddControllers();

            //Cors
            services.AddCorsConfiguration(Configuration);

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // Setting EF Core DBContext
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDatabaseConfiguration(Configuration);

            //AutoMapper
            services.AddAutoMapperConfiguration();

            // .NET Native DI Abstraction
            services.AddDependencyInjectionConfiguration();            
            
            //Application Insights
            //services.AddApplicationInsightsTelemetry();

            //MemoryCache
            services.AddMemoryCache();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);

            var cultureInfo = new CultureInfo("pt-BR");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
            }

            app.UseCorsConfig();


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            SetTimer();
        }
        private Timer _timer;

        //Interval in milliseconds
        int _interval = 5*1000;

        public void SetTimer()
        {
            // this is System.Threading.Timer, of course
            _timer = new Timer(Tick, null, _interval, Timeout.Infinite);
        }

        private void Tick(object state)
        {
            try
            {
                using(HttpClient client = new HttpClient())
                {
                    using HttpResponseMessage response = client.GetAsync("http://192.168.18.6:6006/api/v1/Unit/Verify").Result;
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                }
            }
            finally
            {
                _timer?.Change(_interval, Timeout.Infinite);
            }
        }
    }
}
using GreenerGrain.API.Config;
using GreenerGrain.Framework.StartupBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;

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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
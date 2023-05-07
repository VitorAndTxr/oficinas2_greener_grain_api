using GreenerGrain.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GreenerGrain.API.Config
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var databaseConnectionString = configuration.GetValue<string>("DB:ConnectionString:VirtualScheduleService");            

            services.AddDbContext<DatabaseContext>(options =>
            {
                options
                    .UseNpgsql(databaseConnectionString)                    
                    .UseSnakeCaseNamingConvention();
            });
        }

    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;

namespace GreenerGrain.API.Config
{
    public static class CrossOriginConfig
    {
        const string MyAllowSpecificOrigins = "allowOrigins";

        public static void AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var allowOrigins = JsonConvert.DeserializeObject<string[]>(configuration.GetValue<string>("VirtualScheduleService:Cors:AllowOrigins"));
                        
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins, builder =>
                {
                    builder.WithOrigins(allowOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        public static void UseCorsConfig(this IApplicationBuilder app)
        {
            app.UseCors(MyAllowSpecificOrigins);
        }
    }
}

using GreenerGrain.CrossCutting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GreenerGrain.API.Config
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}

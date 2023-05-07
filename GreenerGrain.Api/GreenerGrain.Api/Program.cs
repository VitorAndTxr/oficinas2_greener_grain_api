using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;

namespace GreenerGrain.API
{
    /// <summary>
    /// Program class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Application Main()
        /// </summary>
        /// <param name="args">The arguments</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// CreateWebHostBuilder
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <returns>The WebHostBuilder</returns>
        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        //.ConfigureKestrel(options =>
                        //{
                        //    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                        //    var isDevelopment = string.Equals(env, "development", StringComparison.InvariantCultureIgnoreCase);
                        //    if (isDevelopment == false)
                        //    {
                        //        var port = Convert.ToInt32(Environment.GetEnvironmentVariable("PORT") ?? "80");
                        //        options.Listen(IPAddress.Any, port);
                        //    }
                        //})
                        .ConfigureAppConfiguration((hostingContext, config) =>
                        {
                            var settings = config.Build();
                        });
                });

    }
}
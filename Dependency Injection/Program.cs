using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Dependency_Injection
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var webHost = CreateHostBuilder(args).Build();

            // use the service provider in the webHost by calling services( the service provider will have been bbuilt using all the services registered in the IServiceCollection)
            // createScope return an IServiceScope: using a scope makes sure that the services expected lifetime is respected.
            using(var scope = webHost.Services.CreateScope())
            {
                // request a serviceProvider from the scope: this serviceProvider is bound to the scope
                var serviceProvider = scope.ServiceProvider;

                // use the ServiceProvider to resolve any dependecies that I need
                var hosttingEnv = serviceProvider.GetRequiredService<IHostingEnvironment>();

                if (hosttingEnv.IsDevelopment())
                {
                    var ctx = serviceProvider.GetRequiredService<AppDbContext>();
                    await ctx.Database.CanConnectAsync();
                }
            }
            // on exiting the using block: the scope is disposed and the services created within the using block will be cleaned off and released for GC.

            webHost.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

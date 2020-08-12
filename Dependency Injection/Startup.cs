using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dependency_Injection.Controllers;
using Dependency_Injection.Implementations;
using Dependency_Injection.Implementations.Notification;
using Dependency_Injection.Interfaces;
using Dependency_Injection.Interfaces.Notification;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Dependency_Injection
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddTransient<IWeatherFocaster, WeatherFocaster>();
            services.AddTransient<IWeatherFocaster, AmazingWeatherFocaster>();
            // doesn't register if an implementation already existfor the type.
            services.TryAddTransient<IWeatherFocaster, AmazingWeatherFocaster>();

            services.AddTransient<IRule, CanDoItRule>();
            services.AddTransient<IRule, MaxNumberRule>();
            services.AddTransient<IRule, OverThresholdRule>();

            // type registration in DI Container
            services.AddTransient<SmsNotification>();
            services.AddTransient<EmailNotification>();

            // register composite services using lambda as the implementation factory argumemt: The Func returns an implementation for the service type of the registration.
            services.AddTransient<INotification>(sp =>
                // creates a new CompositeNotificationService passing an array of INotification items bcos the ctor is expecting IEnumerable of types that implements INotification.
                // whenever an INotificationService instance is resolved from the DI Container it is this CompositeNotificationService instance that will be retuned.
                new CompositeNotificationService(
                    new INotification[]
                    {
                        sp.GetRequiredService<EmailNotification>(),
                        sp.GetRequiredService<SmsNotification>()
                    }));



            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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

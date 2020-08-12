using Dependency_Injection.Controllers;
using Dependency_Injection.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dependency_Injection
{
    public static class ConfigurationServiceCollectionExtension
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IWeatherFocaster, WeatherFocaster>();
            services.AddTransient<IWeatherFocaster, AmazingWeatherFocaster>();

            return services;
        }
    }
}

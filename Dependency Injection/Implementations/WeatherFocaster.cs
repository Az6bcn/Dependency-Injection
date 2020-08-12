using Dependency_Injection.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dependency_Injection.Implementations
{
    public class WeatherFocaster : IWeatherFocaster
    {
        public string GetSeason()
        {
            return "Winter";
        }
    }
}

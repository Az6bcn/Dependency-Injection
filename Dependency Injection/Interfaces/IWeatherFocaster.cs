using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dependency_Injection.Controllers
{
    public interface IWeatherFocaster
    {
        string GetSeason();
    }
}

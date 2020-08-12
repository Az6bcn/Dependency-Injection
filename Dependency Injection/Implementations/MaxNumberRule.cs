using Dependency_Injection.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dependency_Injection.Implementations
{
    public class MaxNumberRule : IRule
    {
        public string PassedRule()
        {
            return $"passed {nameof(MaxNumberRule)} rule";
        }
    }
}

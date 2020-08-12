using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dependency_Injection.Interfaces.Notification
{
    public interface INotification
    {
        Task SendAsync();
    }
}

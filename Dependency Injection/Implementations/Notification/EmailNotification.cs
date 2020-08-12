using Dependency_Injection.Interfaces.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dependency_Injection.Implementations.Notification
{
    public class EmailNotification : INotification
    {
        public Task SendAsync()
        {
            throw new NotImplementedException();
        }
    }
}

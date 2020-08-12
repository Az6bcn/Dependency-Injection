using Dependency_Injection.Interfaces;
using Dependency_Injection.Interfaces.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dependency_Injection.Implementations
{
    public class CompositeNotificationService : INotification
    {
        private readonly IEnumerable<INotification> _compositeNotificationServices;

        public CompositeNotificationService(IEnumerable<INotification> _notificationServices)
        {
            _compositeNotificationServices = _notificationServices;
        }

        public async Task SendAsync()
        {
            foreach (var notificationService in _compositeNotificationServices)
            {
                await notificationService.SendAsync();
            }
        }
    }
}

using Domain.Interfaces.Notification;
using Domain.Services;

namespace API.IoC
{
    public static class DomainIoC
    {
        public static void AddDomainConfigurations(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<INotificationContext, NotificationContext>();
        }
    }
}
using Domain.Interfaces.Notification;

namespace Domain.Services
{
    public class NotificationContext : INotificationContext
    {
        private List<DomainNotification> _notifications = new();
        public IReadOnlyCollection<DomainNotification> Notifications => _notifications;

        public void AddNotification(int code, string title, string message)
        {
            _notifications.Add(new DomainNotification(code, title, message));
        }

        public void AddNotifier(INotifier notifier)
        {
            _notifications = notifier.Errors.ToList();
        }

        public void Clear()
        {
            _notifications.Clear();
        }

        public bool HasNotifications() => _notifications.Any();
    }
}
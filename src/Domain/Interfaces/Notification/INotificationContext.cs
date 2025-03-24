namespace Domain.Interfaces.Notification
{
    public interface INotificationContext
    {
        IReadOnlyCollection<DomainNotification> Notifications { get; } 
        
        void AddNotification(int code, string key, string message);

        void AddNotifier(INotifier notifier);

        bool HasNotifications();

        void Clear();
    }
}
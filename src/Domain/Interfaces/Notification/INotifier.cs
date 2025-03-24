namespace Domain.Interfaces.Notification
{
    public interface INotifier
    {
        IList<DomainNotification> Errors { get; }
    }
}
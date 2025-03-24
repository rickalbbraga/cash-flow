using Domain.Interfaces.Notification;

namespace Domain.Entities
{
    public abstract class Entity : INotifier
    {
        public Guid Id { get; protected set; }

        public IList<DomainNotification> Errors { get; protected set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
            Errors = [];
        }

        protected abstract void Validate();
    }
}
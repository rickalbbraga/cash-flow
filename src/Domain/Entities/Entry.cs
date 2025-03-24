using Domain.Enums;
using Domain.Interfaces.Notification;
using Domain.Utils;

namespace Domain.Entities
{
    public class Entry : Entity
    {
        public DateTime Date { get; private set; }
        public string? Description { get; private set; }
        public decimal Value { get; private set; }
        public EntryType Type { get; private set; }

        private Entry()
        {
        }

        private Entry(DateTime date, string? description, decimal value, EntryType type)
        {
            Date = date;
            Description = description;
            Value = value;
            Type = type;

            Validate();
        }

        public static Entry Create(DateTime date, string? description, decimal value, EntryType type)
            => new Entry(date, description, value, type);


        public void Update(DateTime date, string? description, decimal value, EntryType type)
        {
            Date = date;
            Description = description;
            Value = value;
            Type = type;

            Validate();
        }

        protected override void Validate()
        {
            if (Date == DateTime.MinValue || Date >= DateTime.Now)
            {
                Errors.Add(new DomainNotification(DomainErrorMessage.BadRequestErrorCode, DomainErrorMessage.TitleErrorMessage, DomainErrorMessage.InvalidDateErrorMessage));
            }

            if (string.IsNullOrEmpty(Description))
            {
                Errors.Add(new DomainNotification(DomainErrorMessage.BadRequestErrorCode, DomainErrorMessage.TitleErrorMessage, DomainErrorMessage.RequiredDescriptionErrorMessage));
            }

            if (Value <= 0)
            {
                Errors.Add(new DomainNotification(DomainErrorMessage.BadRequestErrorCode, DomainErrorMessage.TitleErrorMessage, DomainErrorMessage.ValueErrorMessage));
            }

            if (!Enum.IsDefined(typeof(EntryType), Type))
            {
                Errors.Add(new DomainNotification(DomainErrorMessage.BadRequestErrorCode, DomainErrorMessage.TitleErrorMessage, DomainErrorMessage.TypeErrorMessage));
            }
        }
    }
}
namespace Domain.Interfaces.Notification
{
    public sealed class DomainNotification
    {
        public int Code { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }

        public DomainNotification(int code, string? title, string? message)
        {
            Code = code;
            Title = title;
            Message = message;
        }
    }
}
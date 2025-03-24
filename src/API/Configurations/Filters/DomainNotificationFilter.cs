using System.Text.Json;
using Application.Results;
using Domain.Interfaces.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Configurations.Filters
{
    public class DomainNotificationFilter(
        ILogger<DomainNotificationFilter> logger,
        INotificationContext notificationContext
    ) : IActionFilter
    {
        private readonly ILogger<DomainNotificationFilter> _logger = logger;
        private readonly INotificationContext _notificationContext = notificationContext;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (_notificationContext.HasNotifications()) 
            {
                var response = BuildResponse();
                _logger.LogWarning(JsonSerializer.Serialize(response));
                context.Result = new BadRequestObjectResult(response);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _notificationContext.Clear();
        }

        public List<ErrorResponse> BuildResponse() 
        {
            return _notificationContext.Notifications.Select(n => new ErrorResponse 
            {
                Code = n.Code,
                Title = n.Title,
                Message = n.Message
            }).ToList();
        }
    }
}
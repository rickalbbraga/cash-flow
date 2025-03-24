using System.Net;
using System.Text.Json;
using Application.Results;

namespace API.Configurations.Middlewares
{
    public class ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger
    )
    {
        private const int InternalServerErrorCode = 50000;
        private const string InternalServerErrorTitle = "Erro Interno";
        private const string InternalServerErrorMessage = "Um erro inesperado aconteceu.";
        private const string ApplicationJson = "application/json";


        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, InternalServerErrorMessage);
                await HandleExceptionAsync(context);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context)
        {
            var response = new ErrorResponse
            {
                Code = InternalServerErrorCode,
                Title = InternalServerErrorTitle,
                Message = InternalServerErrorMessage,
            };

            context.Response.ContentType = ApplicationJson;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
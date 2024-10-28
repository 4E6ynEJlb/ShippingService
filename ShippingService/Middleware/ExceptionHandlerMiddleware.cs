using System.Net;
using System.Text.Json;
using Application.Models;
using Domain.Models;

namespace API.Middleware
{
    internal class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ServiceException serviceException)
            {
                await HandleServiceException(context, serviceException);
            }
            catch (Exception exception)
            {
                await HandleUnknownException(context, exception);
            }
        }

        private Task HandleServiceException(HttpContext context, ServiceException exception)
        {
            _logger.LogWarning($"{LoggerMessages.SERVICE_EXCEPTION}{exception.Message}");
            int statusCode = (int)exception.StatusCode;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            string result = JsonSerializer.Serialize(new
            {
                StatusCode = statusCode,
                ErrorMessage = exception.Message
            });
            return context.Response.WriteAsync(result);
        }

        private Task HandleUnknownException(HttpContext context, Exception exception)
        {
            _logger.LogError($"{LoggerMessages.UNKNOWN_EXCEPTION}{exception.Message}");
            int statusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            string result = JsonSerializer.Serialize(new
            {
                StatusCode = statusCode,
                ErrorMessage = exception.Message
            });
            return context.Response.WriteAsync(result);
        }
    }
}

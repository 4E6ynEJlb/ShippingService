using System.Net;

namespace Domain.Models
{
    public abstract class ServiceException(string message, HttpStatusCode statusCode) : Exception(message)
    {
        public HttpStatusCode StatusCode { get; set; } = statusCode;
    }
}

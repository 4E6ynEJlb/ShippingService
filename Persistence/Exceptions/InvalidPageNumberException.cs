using Domain.Models;
using System.Net;

namespace Persistence.Exceptions
{
    public class InvalidPageNumberException : ServiceException
    {
        public InvalidPageNumberException() :
            base(ErrorsMessages.INVALID_PAGE, HttpStatusCode.BadRequest)
        { }
    }
}
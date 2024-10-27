using Domain.Models;
using System.Net;

namespace Application.Exceptions
{
    public class EmptyArgumentException : ServiceException
    {
        public EmptyArgumentException() : 
            base(ErrorsMessages.EMPTY_ARGUMENT, HttpStatusCode.BadRequest) 
        { }
    }
}

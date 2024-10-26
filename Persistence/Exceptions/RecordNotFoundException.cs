using Domain.Models;
using System.Net;

namespace Persistence.Exceptions
{
    public class RecordNotFoundException : ServiceException
    {
        public RecordNotFoundException() :
            base(ErrorsMessages.RECORD_NOT_FOUND, HttpStatusCode.NotFound)
        { }
    }
}
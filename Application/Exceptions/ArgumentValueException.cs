using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ArgumentValueException : ServiceException
    {
        public ArgumentValueException() :
            base(ErrorsMessages.ARGUMENT_VALUE, HttpStatusCode.BadRequest)
        { }
    }
}

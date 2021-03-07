using Shared.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Common
{
    public class Response
    {
        public string Message { get; set; }
        public string ExceptionMessaage { get; set; }
        public string InnerExceptionMessage { get; set; }

        public Response(ResponseMessageType responseMessageType, Exception exception = null)
            : this(responseMessageType.GetName(), exception) { }

        public Response(string message, Exception exception = null)
        {
            Message = message;
            ExceptionMessaage = exception?.Message;
            InnerExceptionMessage = exception?.InnerException?.Message;
        }
    }
}

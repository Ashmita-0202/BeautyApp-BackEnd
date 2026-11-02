using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TN.Shared.Infrastructure.CustomMiddleware.CustomException
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
            
        }
        public NotFoundException(string message) : base(message)
        {
            
        }

        //Define the property of the status code
        public HttpStatusCode StatusCode { get; } = HttpStatusCode.NotFound;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace ZendeskApi.Client.Exceptions
{
    public class ZendeskRequestException : Exception
    {
        public ErrorResponse Error { get; }

        internal ZendeskRequestException(string message, ErrorResponse errorResponse) : base(message)
        {
            Error = errorResponse;
        }
    }
}


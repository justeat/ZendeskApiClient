using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;

namespace ZendeskApi.Client.Exceptions
{
    public class ZendeskRequestException : Exception
    {
        public ErrorResponse Error { get; }
        public HttpResponseMessage Response { get; }

        internal ZendeskRequestException(string message, ErrorResponse errorResponse, HttpResponseMessage response) : base(message)
        {
            Error = errorResponse;
            Response = response;
        }
    }
}


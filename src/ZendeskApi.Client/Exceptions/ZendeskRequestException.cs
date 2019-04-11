using System;
using System.Net.Http;

namespace ZendeskApi.Client.Exceptions
{
    public class ZendeskRequestException : Exception
    {
        public ErrorResponse Error { get; }
        public HttpResponseMessage Response { get; }

        public ZendeskRequestException(
            string message, 
            ErrorResponse errorResponse, 
            HttpResponseMessage response) 
            : base(message)
        {
            Error = errorResponse;
            Response = response;
        }
    }
}


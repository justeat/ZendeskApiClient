using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ZendeskApi.Client.Http
{
    public interface IHttpRequest
    {
        HttpMethod Method { get; set; }

        Uri RequestUri { get; }

        IEnumerable<KeyValuePair<string, string>> Headers { get; }

        string Content { get; }

        string ContentType { get; }

        TimeSpan? Timeout { get; set; }
    }
}
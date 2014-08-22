using System.Collections.Generic;
using System.Net;

namespace JustEat.ZendeskApi.Client.Http
{
    public interface IHttpResponse
    {
        IDictionary<string, string> Headers { get; set; }

        HttpStatusCode StatusCode { get; set; }

        bool IsSuccessStatusCode { get; }

        string ReasonPhrase { get; set; }

        string Content { get; set; }
    }
}
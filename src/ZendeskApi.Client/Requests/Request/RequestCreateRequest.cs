using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    internal class RequestCreateRequest
    {
        public RequestCreateRequest(Request request)
        {
            Request = request;
        }

        [JsonProperty("request")]
        public Request Request { get; set; }
    }
}
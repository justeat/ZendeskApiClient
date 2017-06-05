using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    public class RequestRequest : IRequest<Request>
    {
        [JsonProperty("request")]
        public Request Item { get; set; }
    }
}

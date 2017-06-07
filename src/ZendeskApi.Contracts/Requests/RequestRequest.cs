using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    public class RequestRequest
    {
        [JsonProperty("request")]
        public Request Item { get; set; }
    }
}

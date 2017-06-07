using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class RequestResponse
    {
        [JsonProperty("request")]
        public Request Item { get; set; }
    }
}

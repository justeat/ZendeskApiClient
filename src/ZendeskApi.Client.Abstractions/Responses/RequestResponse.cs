using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class RequestResponse
    {
        [JsonProperty("request")]
        public Request Item { get; set; }
    }
}

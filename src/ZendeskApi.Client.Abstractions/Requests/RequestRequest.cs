using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    public class RequestRequest
    {
        [JsonProperty("request")]
        public Request Item { get; set; }
    }
}

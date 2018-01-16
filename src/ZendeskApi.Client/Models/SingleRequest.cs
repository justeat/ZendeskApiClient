using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    public class SingleRequest
    {
        [JsonProperty("request")]
        public Request Request { get; set; }
    }
}
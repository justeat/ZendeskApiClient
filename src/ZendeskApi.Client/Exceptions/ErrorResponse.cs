using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi.Client.Exceptions
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
        }

        [JsonProperty("error")]
        public string Error { get; internal set; }

        [JsonProperty("description")]
        public string Description { get; internal set; }

        [JsonProperty("details")]
        public JObject Details { get; internal set; }
    }
}
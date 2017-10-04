using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class UploadResponse
    {
        [JsonProperty("upload")]
        public Upload Upload { get; set; }
    }
}

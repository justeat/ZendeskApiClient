using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class UploadResponse
    {
        [JsonProperty("upload")]
        public Upload Item { get; set; }
    }
}

using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class AttachmentResponse
    {
        [JsonProperty("attachment")]
        public Attachment Item { get; set; }
    }
}

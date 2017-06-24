using System;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    [JsonObject("upload")]
    public class Upload
    {
        [JsonIgnore]
        public long? Id { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("expires_at")]
        public DateTime ExpiresAt { get; set; }

        [JsonProperty("attachment")]
        public Attachment Attachment { get; set; }
    }
}

using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ZendeskApi.Contracts.Models
{
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

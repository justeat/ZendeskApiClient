using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class AuditEvent
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("type")]
        public AuditTypes Type { get; set; }
    }
}
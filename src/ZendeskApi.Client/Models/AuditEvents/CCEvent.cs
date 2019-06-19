using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class CCEvent : AuditEvent, IAuditEvent
    {
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("recipients")]
        public long[] Recipients { get; set; }
        [JsonProperty("via")]
        public Via Via { get; set; }
    }
}
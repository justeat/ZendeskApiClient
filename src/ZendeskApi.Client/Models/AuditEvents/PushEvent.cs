using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class PushEvent : AuditEvent
    {
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("value_reference")]
        public string ValueReference { get; set; }
    }
}
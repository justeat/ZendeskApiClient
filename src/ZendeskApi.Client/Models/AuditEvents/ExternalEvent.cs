using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class ExternalEvent : AuditEvent, IAuditEvent
    {
        [JsonProperty("resource")]
        public string Resource { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
    }
}
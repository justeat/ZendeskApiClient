using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class LogMeInTranscriptEvent : AuditEvent, IAuditEvent
    {
        [JsonProperty("body")]
        public string Body { get; set; }
    }
}
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class ErrorEvent : AuditEvent, IAuditEvent
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
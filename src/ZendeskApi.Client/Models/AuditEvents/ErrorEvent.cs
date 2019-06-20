using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class ErrorEvent : AuditEvent
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class FacebookEvent : AuditEvent, IAuditEvent
    {
        [JsonProperty("communication")]
        public int Communication { get; set; }
        [JsonProperty("ticket_via")]
        public string TicketVia { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
        
    }
}
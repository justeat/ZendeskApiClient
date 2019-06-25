using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class TicketSharingEvent : AuditEvent
    {
        [JsonProperty("agreement_id")]
        public int AgreementId { get; set; }
        [JsonProperty("action")]
        public string Action { get; set; }
    }
}
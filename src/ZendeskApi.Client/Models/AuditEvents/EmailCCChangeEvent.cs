using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class EmailCCChangeEvent : AuditEvent, IAuditEvent
    {
        [JsonProperty("previous_email_ccs")]
        public string[] PreviousEmailCCs { get; set; }
        [JsonProperty("current_email_ccs")]
        public string[] CurrentEmailCCs { get; set; }
    }
}
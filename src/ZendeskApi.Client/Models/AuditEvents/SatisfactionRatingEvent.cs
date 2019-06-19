using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class SatisfactionRatingEvent : AuditEvent, IAuditEvent
    {
        [JsonProperty("score")]
        public string Score { get; set; }
        [JsonProperty("assignee_id")]
        public long AssigneeId { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
    }
}
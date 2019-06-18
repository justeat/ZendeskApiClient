using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class CommentPrivacyChangeEvent : AuditEvent, IAuditEvent
    {
        [JsonProperty("comment_id")]
        public int CommentId { get; set; }
        [JsonProperty("public")]
        public bool Public { get; set; }
    }
}
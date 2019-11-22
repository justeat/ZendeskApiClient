using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class CommentPrivacyChangeEvent : AuditEvent
    {
        [JsonProperty("comment_id")]
        public long CommentId { get; set; }
        [JsonProperty("public")]
        public bool Public { get; set; }
    }
}
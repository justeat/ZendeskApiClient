using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class CommentRedactionEvent : AuditEvent
    {
        [JsonProperty("comment_id")]
        public long CommentId { get; set; }
    }
}
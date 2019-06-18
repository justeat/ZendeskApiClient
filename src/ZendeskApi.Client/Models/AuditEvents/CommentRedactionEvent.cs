using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class CommentRedactionEvent : AuditEvent, IAuditEvent
    {
        [JsonProperty("comment_id")]
        public int CommentId { get; set; }
    }
}
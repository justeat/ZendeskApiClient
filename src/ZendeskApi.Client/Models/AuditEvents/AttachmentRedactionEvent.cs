using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class AttachmentRedactionEvent : AuditEvent, IAuditEvent
    {
        [JsonProperty("attachment_id")]
        public int AttachmentId { get; set; }
        [JsonProperty("comment_id")]
        public int CommentId { get; set; }
    }
}
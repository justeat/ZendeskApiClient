using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class AttachmentRedactionEvent : AuditEvent
    {
        [JsonProperty("attachment_id")]
        public long AttachmentId { get; set; }
        [JsonProperty("comment_id")]
        public long CommentId { get; set; }
    }
}
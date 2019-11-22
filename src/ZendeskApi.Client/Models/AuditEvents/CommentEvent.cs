using System;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class CommentEvent : AuditEvent
    {
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("html_body")]
        public string HtmlBody { get; set; }
        [JsonProperty("plain_body")]
        public string PlainBody { get; set; }
        [JsonProperty("public")]
        public bool Public { get; set; }
        [JsonProperty("author_id")]
        public long AuthorId { get; set; }
        [JsonProperty("attachments")]
        public Attachment[] Attachments { get; set; }
        [JsonProperty("via")]
        public Via Via { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class VoiceCommentEvent : AuditEvent
    {
        [JsonProperty("data")]
        public object Data { get; set; }
        [JsonProperty("formatted_from")]
        public string FormattedFrom { get; set; }
        [JsonProperty("formatted_to")]
        public string FormattedTo { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("html_body")]
        public string HtmlBody { get; set; }
        [JsonProperty("public")]
        public bool Public { get; set; }
        [JsonProperty("trusted")]
        public bool Trusted { get; set; }
        [JsonProperty("author_id")]
        public long AuthorId { get; set; }
        [JsonProperty("transcription_visible")]
        public bool TranscriptionVisible { get; set; }
        [JsonProperty("attachments")]
        public Attachment[] Attachments { get; set; }
    }
}
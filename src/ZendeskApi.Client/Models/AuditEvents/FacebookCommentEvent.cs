using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class FacebookCommentEvent : AuditEvent
    {
        [JsonProperty("data")]
        public object Data { get; set; }
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
        [JsonProperty("graph_object_id")]
        public string GraphObjectId { get; set; }
    }
}
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class TweetEvent : AuditEvent, IAuditEvent
    {
        [JsonProperty("direct_message")]
        public bool DirectMessage { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("recipients")]
        public int[] Recipients { get; set; }
    }
}
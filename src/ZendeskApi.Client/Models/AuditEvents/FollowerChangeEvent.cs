using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class FollowerChangeEvent : AuditEvent, IAuditEvent
    {
        [JsonProperty("previous_followers")]
        public string[] PreviousFollowers { get; set; }
        [JsonProperty("current_followers")]
        public string[] CurrentFollowers { get; set; }
    }
}
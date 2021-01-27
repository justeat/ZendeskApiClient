using Newtonsoft.Json;

namespace ZendeskApi.Client.Responses
{
    public class UserRelatedInformationResponse
    {
        [JsonProperty("assigned_tickets")]
        public int AssignedTickets { get; set; }

        [JsonProperty("requested_tickets")]
        public int RequestedTickets { get; set; }

        [JsonProperty("ccd_tickets")]
        public int CcdTickets { get; set; }

        [JsonProperty("organization_subscriptions")]
        public int OrganizationSubscriptions { get; set; }

        [JsonProperty("topics")]
        public int Topics { get; set; }

        [JsonProperty("topic_comments")]
        public int TopicComments { get; set; }

        [JsonProperty("votes")]
        public int Votes { get; set; }

        [JsonProperty("subscriptions")]
        public int Subscriptions { get; set; }

        [JsonProperty("entry_subscriptions")]
        public int EntrySubscriptions { get; set; }

        [JsonProperty("forum_subscriptions")]
        public int ForumSubscriptions { get; set; }
    }
}
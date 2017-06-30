using System;
using System.Reflection;
using Newtonsoft.Json;
using ZendeskApi.Client.Models.Tickets;

namespace ZendeskApi.Client.Models
{
    public class Ticket : BaseTicket, ISearchResult
    {
        [JsonProperty]
        public long Id { get; set; }

        [JsonProperty]
        public Uri Url { get; set; }

        [JsonProperty("requester_id")]
        public long RequesterId { get; set; }

        [JsonProperty("submitter_id")]
        public long SubmitterId { get; set; }
        
        [JsonProperty("organization_id")]
        public long OrganisationId { get; set; }

        [JsonProperty("has_incidents")]
        public bool HasIncidents { get; set; }
        
        [JsonProperty("ticket_form_id")]
        public long FormId { get; set; }

        [JsonProperty("brand_id")]
        public long BrandId { get; set; }

        [JsonProperty("allow_channelback")]
        public bool AllowChannelback { get; set; }

        [JsonProperty("is_public")]
        public bool IsPublic { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        
        DateTime ISearchResult.CreatedAt => CreatedAt;
        DateTime ISearchResult.UpdatedAt => UpdatedAt;
        long ISearchResult.Id => Id;
        Uri ISearchResult.Url => Url;

        [JsonProperty("result_type")]
        string ISearchResult.Type => typeof(Ticket).GetTypeInfo().GetCustomAttribute<JsonObjectAttribute>().Id;
    }
}

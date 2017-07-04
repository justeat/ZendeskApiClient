using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Models
{
    [JsonObject("organization")]
    public class Organization : ISearchResponse
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("due_at")]
        public DateTime? Due { get; set; }

        [JsonProperty("organization_fields")]
        public Dictionary<object, object> CustomFields { get; set; }
        
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
        
        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("domain_names")]
        public List<string> DomainNames { get; set; }

        [JsonIgnore]
        [JsonProperty("shared_tickets")]
        public bool SharedTickets { get; set; }

        [JsonIgnore]
        [JsonProperty("shared_comments")]
        public bool SharedComments { get; set; }

        [JsonIgnore]
        public Uri Url { get; set; }

        [JsonIgnore]
        [JsonProperty("group_id")]
        public long GroupId { get; set; }

        DateTime ISearchResponse.CreatedAt => CreatedAt.Value;
        DateTime ISearchResponse.UpdatedAt => UpdatedAt.Value;
        long ISearchResponse.Id => Id.Value;
        Uri ISearchResponse.Url => Url;

        [JsonProperty("result_type")]
        string ISearchResponse.Type => typeof(Organization).GetTypeInfo().GetCustomAttribute<JsonObjectAttribute>().Id;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace ZendeskApi.Contracts.Models
{
    [Description("Organization")]
    public class Organization : IZendeskEntity
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
        public DateTime? Created { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? Updated { get; set; }

        [JsonProperty("due_at")]
        public DateTime? Due { get; set; }

        [JsonProperty("organization_fields")]
        public Dictionary<object, object> CustomFields { get; set; }
        
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
        
        [JsonProperty("external_id")]
        public string external_id { get; set; }

        [JsonProperty("domain_names")]
        public List<string> DomainNames { get; set; }

        [JsonIgnore]
        public bool shared_tickets { get; set; }

        [JsonIgnore]
        public bool shared_comments { get; set; }

        [JsonIgnore]
        public Uri Url { get; set; }

        [JsonIgnore]
        public long group_id { get; set; }
    }
}

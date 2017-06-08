using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    [Description("Group")]
    public class Group
    {
        [JsonProperty]
        public long? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created_at")]
        public DateTime? Created { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? Updated { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("has_incidents")]
        public bool HasIncidents { get; set; }
    }
}

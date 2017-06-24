using System;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    [JsonObject("group", Title = "group")]
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

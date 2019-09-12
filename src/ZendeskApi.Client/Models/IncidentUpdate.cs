using System;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    public class IncidentUpdate
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("incident_id")]
        public string IncidentId { get; set; }
    }
}
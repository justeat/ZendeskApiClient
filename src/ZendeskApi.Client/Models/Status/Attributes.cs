using System;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Status
{
    public class Attributes
    {
        [JsonProperty("degradation")]
        public bool Degradation { get; set; }

        [JsonProperty("impact")]
        public Impact Impact { get; set; }

        [JsonProperty("outage")]
        public bool Outage { get; set; }

        [JsonProperty("postmortem")]
        public string Postmortem { get; set; }

        [JsonProperty("resolved_at")]
        public DateTime? ResolvedAt { get; set; }

        [JsonProperty("started_at")]
        public DateTime StartedAt { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("incident_id")]
        public string IncidentId { get; set; }

        [JsonProperty("service_id")]
        public string ServiceId { get; set; }
    }
}
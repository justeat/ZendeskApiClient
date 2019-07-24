using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    public class Incident
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("started_at")]
        public DateTime StartedAt { get; set; }

        [JsonProperty("status")]
        public ComponentStatus Status { get; set; }

        [JsonProperty("impact")]
        public ComponentImpact Impact { get; set; }

        [JsonProperty("updates")]
        public IReadOnlyList<IncidentUpdate> Updates { get; set; }

        [JsonProperty("components")]
        public IReadOnlyList<Component> Components { get; set; }

        [JsonProperty("subcomponents")]
        public IReadOnlyList<SubComponent> SubComponents { get; set; }
    }
}
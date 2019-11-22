using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    public class ServiceStatus
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("component_id")]
        public int? ComponentId { get; set; }

        [JsonProperty("pod_id")]
        public int? PodId { get; set; }

        [JsonProperty("status")]
        public ComponentStatus Status { get; set; }

        [JsonProperty("impact")]
        public ComponentImpact Impact { get; set; }

        [JsonProperty("active_incidents")]
        public IReadOnlyList<Incident> ActiveIncidents { get; set; }
    }
}

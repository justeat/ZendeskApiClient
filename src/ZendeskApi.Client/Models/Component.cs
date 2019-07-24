using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    public class ComponentResponse
    {
        [JsonProperty("components")]
        public Component[] Components { get; set; }
    }

    public class Component
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
        public int PodId { get; set; }

        [JsonProperty("subcomponents")]
        public IReadOnlyList<SubComponent> SubComponents { get; set; }
    }
}
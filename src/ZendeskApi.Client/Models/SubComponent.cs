using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    public class SubComponentResponse
    {
        [JsonProperty("subcomponents")]
        public SubComponent[] SubComponents { get; set; }
    }

    public class SubComponent
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
    }
}
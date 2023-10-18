using Newtonsoft.Json;
namespace ZendeskApi.Client.Models
{
    public class Tag 
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}

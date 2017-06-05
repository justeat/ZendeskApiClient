using System.ComponentModel;
using Newtonsoft.Json;

namespace ZendeskApi.Contracts.Models
{
    [Description("custom_field_option")]
    public class CustomFieldOption
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("raw_name")]
        public string RawName { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}

using System.ComponentModel;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    [Description("custom_field_option")]
    public class CustomFieldOption
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("raw_name")]
        public string RawName { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}

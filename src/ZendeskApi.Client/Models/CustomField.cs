using System.ComponentModel;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    [Description("custom_field")]
    public class CustomField
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}

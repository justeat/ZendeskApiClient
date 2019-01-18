using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using ZendeskApi.Client.Converters;

namespace ZendeskApi.Client.Models
{
    [Description("custom_field")]

    public class CustomField
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("value")]
        [JsonConverter(typeof(CustomFieldValueConverter))]
        public List<string> Value { get; set; }
    }
}

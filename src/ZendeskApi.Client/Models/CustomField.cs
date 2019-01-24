using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using ZendeskApi.Client.Converters;

namespace ZendeskApi.Client.Models
{
    [Description("custom_field")]
    [JsonConverter(typeof(CustomFieldConverter))]
    public class CustomField
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        public string Value { get; set; }

        public List<string> Values { get; set; }
    }
}

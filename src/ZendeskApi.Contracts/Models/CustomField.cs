using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ZendeskApi.Contracts.Models
{
    [Description("custom_field")]
    [DebuggerDisplay("Id:{Id} Value:{Value}")]
    [DataContract]
    public class CustomField
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(Name = "value", EmitDefaultValue = false)]
        [JsonConverter(typeof(CustomFieldBoolConverter))]
        public string Value { get; set; }
    }
}

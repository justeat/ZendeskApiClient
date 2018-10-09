using System.ComponentModel;
using System.Runtime.Serialization;

namespace ZendeskApi.Contracts.Models
{
    [Description("custom_field_option")]
    [DataContract]
    public class CustomFieldOption
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "raw_name")]
        public string RawName { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}

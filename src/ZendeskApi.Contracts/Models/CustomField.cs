using System.ComponentModel;
using System.Runtime.Serialization;

namespace ZendeskApi.Contracts.Models
{
    [Description("custom_field")]
    [DataContract]
    public class CustomField
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JustEat.ZendeskApi.Contracts.Models
{
    [DataContract]
    public class TicketCustomField
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }
}

using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JustEat.ZendeskApi.Contracts.Models
{
    [DataContract]
    public enum TicketPriority
    {
        [EnumMember(Value = "urgent")]
        Urgent,
        [EnumMember(Value = "high")]
        High,
        [EnumMember(Value = "normal")]
        Normal,
        [EnumMember(Value = "low")]
        Low,

    }
}
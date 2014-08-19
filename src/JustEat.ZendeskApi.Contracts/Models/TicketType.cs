using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JustEat.ZendeskApi.Contracts.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TicketType
    {
        [EnumMember(Value = "task")]
        Task,
        [EnumMember(Value = "incident")]
        Incident,
        [EnumMember(Value = "problem")]
        Problem,
        [EnumMember(Value = "question")]
        Question
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi.Contracts.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TicketType
    {
        task,
        incident,
        problem,
        question
    }
}

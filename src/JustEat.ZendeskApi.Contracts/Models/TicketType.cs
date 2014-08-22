using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JustEat.ZendeskApi.Contracts.Models
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

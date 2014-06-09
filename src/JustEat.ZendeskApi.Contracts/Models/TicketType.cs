using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JustEat.ZendeskApi.Contracts.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TicketType
    {
        Task,
        Incident,
        Problem,
        Question
    }
}

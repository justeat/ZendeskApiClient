using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JustEat.ZendeskApi.Contracts.Models
{
    [JsonConverter(typeof (StringEnumConverter))]
    public enum TicketStatus
    {
        Open,
        Closed,
        New,
        Pending,
        Solved
    }
}
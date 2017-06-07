using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class TicketFieldResponse
    {
        [JsonProperty("ticket_field")]
        public TicketField Item { get; set; }
    }
}

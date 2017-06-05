using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class TicketFieldResponse : IResponse<TicketField>
    {
        [JsonProperty("ticket_field")]
        public TicketField Item { get; set; }
    }
}

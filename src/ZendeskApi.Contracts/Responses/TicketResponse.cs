using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class TicketResponse : IResponse<Ticket>
    {
        [JsonProperty("ticket")]
        public Ticket Item { get; set; }
    }
}

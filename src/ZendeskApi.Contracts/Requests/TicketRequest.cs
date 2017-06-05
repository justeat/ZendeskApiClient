using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    public class TicketRequest : IRequest<Ticket>
    {
        [JsonProperty("ticket")]
        public Ticket Item { get; set; }
    }
}

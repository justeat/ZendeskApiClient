using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    public class TicketRequest
    {
        [JsonProperty("ticket")]
        public Ticket Item { get; set; }
    }

    public class TicketsRequest
    {
        [JsonProperty("tickets")]
        public IEnumerable<Ticket> Item { get; set; }
    }
}

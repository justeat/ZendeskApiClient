using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class TicketsListResponse : PaginationResponse<Ticket>
    {
        [JsonProperty("tickets")]
        public IEnumerable<Ticket> Tickets { get; set; }

        protected override IEnumerable<Ticket> Enumerable => Tickets;
    }
}

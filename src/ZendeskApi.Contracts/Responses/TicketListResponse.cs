using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class TicketListResponse : ListResponse<Ticket>
    {
        [JsonProperty("tickets")]
        public override IEnumerable<Ticket> Results { get; set; }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class DeletedTicketsListCursorResponse : CursorPaginationResponse<Ticket>
    {
        [JsonProperty("deleted_tickets")]
        public IEnumerable<Ticket> Tickets { get; set; }

        protected override IEnumerable<Ticket> Enumerable => Tickets;
    }
}

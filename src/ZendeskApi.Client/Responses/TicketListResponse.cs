using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class TicketListResponse : PaginationResponse<TicketResponse>
    {
        [JsonProperty("tickets")]
        public IEnumerable<TicketResponse> Tickets { get; set; }


        protected override IEnumerable<TicketResponse> Enumerable => Tickets;
    }
}

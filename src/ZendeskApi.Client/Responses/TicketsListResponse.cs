using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class TicketsListResponse : PaginationResponse<TicketResponse>
    {
        [JsonProperty("tickets")]
        public override IEnumerable<TicketResponse> Item { get; set; }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Responses
{
    [JsonObject]
    public class TicketsListResponse : PaginationResponse<TicketResponse>
    {
        [JsonProperty("tickets")]
        public override IEnumerable<TicketResponse> Item { get; set; }
    }
}

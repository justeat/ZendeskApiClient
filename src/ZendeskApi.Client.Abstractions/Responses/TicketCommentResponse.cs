using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class TicketCommentsResponse : PaginationResponse<TicketComment>
    {
        [JsonProperty("comments")]
        public override IEnumerable<TicketComment> Item { get; set; }
    }
}

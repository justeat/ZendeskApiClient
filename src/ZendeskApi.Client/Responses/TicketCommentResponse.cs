using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Responses
{
    [JsonObject]
    public class TicketCommentsResponse : PaginationResponse<TicketComment>
    {
        [JsonProperty("comments")]
        public override IEnumerable<TicketComment> Item { get; set; }
    }
}

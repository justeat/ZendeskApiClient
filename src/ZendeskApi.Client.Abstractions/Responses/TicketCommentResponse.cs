using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class TicketCommentResponse
    {
        [JsonProperty("comment")]
        public TicketComment Item { get; set; }
    }

    [JsonObject]
    public class TicketCommentsResponse : PaginationResponse<TicketComment>
    {
        [JsonProperty("comments")]
        public override IEnumerable<TicketComment> Item { get; set; }
    }
}

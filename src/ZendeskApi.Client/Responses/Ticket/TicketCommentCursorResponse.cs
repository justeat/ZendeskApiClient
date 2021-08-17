using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class TicketCommentsCursorResponse : CursorPaginationResponse<TicketComment>
    {
        [JsonProperty("comments")]
        public IEnumerable<TicketComment> Comments { get; set; }
        
        protected override IEnumerable<TicketComment> Enumerable => Comments;
    }
}

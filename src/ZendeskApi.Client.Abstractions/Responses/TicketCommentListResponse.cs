using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class TicketCommentListResponse : ListResponse<TicketComment>
    {
        [JsonProperty("comments")]
        public override IEnumerable<TicketComment> Results { get; set; }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class TicketCommentListResponse : ListResponse<TicketComment>
    {
        [JsonProperty("comments")]
        public override IEnumerable<TicketComment> Results { get; set; }
    }
}

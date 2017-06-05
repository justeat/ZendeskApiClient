using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class TicketFieldListResponse : ListResponse<TicketField>
    {
        [JsonProperty("ticket_fields")]
        public override IEnumerable<TicketField> Results { get; set; }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class TicketFieldListResponse : ListResponse<TicketField>
    {
        [JsonProperty("ticket_fields")]
        public override IEnumerable<TicketField> Results { get; set; }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class TicketFieldsResponse : PaginationResponse<TicketField>
    {
        [JsonProperty("ticket_fields")]
        public override IEnumerable<TicketField> Item { get; set; }
    }
}

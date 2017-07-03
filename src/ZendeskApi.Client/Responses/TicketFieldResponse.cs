using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Responses
{
    [JsonObject]
    public class TicketFieldsResponse : PaginationResponse<TicketField>
    {
        [JsonProperty("ticket_fields")]
        public override IEnumerable<TicketField> Item { get; set; }
    }
}

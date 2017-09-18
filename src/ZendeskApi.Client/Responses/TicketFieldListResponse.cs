using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class TicketFieldListResponse : PaginationResponse<TicketField>
    {
        [JsonProperty("ticket_fields")]
        public IEnumerable<TicketField> TicketFields { get; set; }

        protected override IEnumerable<TicketField> Enumerable => TicketFields;
    }
}

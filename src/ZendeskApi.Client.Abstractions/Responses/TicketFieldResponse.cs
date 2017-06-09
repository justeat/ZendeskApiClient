using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class TicketFieldResponse
    {
        [JsonProperty("ticket_field")]
        public TicketField Item { get; set; }
    }

    public class TicketFieldsResponse
    {
        [JsonProperty("ticket_fields")]
        public IEnumerable<TicketField> Item { get; set; }
    }
}

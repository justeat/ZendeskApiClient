using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class TicketFieldsResponse
    {
        [JsonProperty("ticket_fields")]
        public IEnumerable<TicketField> Item { get; set; }
    }
}

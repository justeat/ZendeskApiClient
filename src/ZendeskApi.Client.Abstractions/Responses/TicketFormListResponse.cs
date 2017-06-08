using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class TicketFormListResponse : ListResponse<TicketForm>
    {
        [JsonProperty("ticket_forms")]
        public override IEnumerable<TicketForm> Results { get; set; }
    }
}

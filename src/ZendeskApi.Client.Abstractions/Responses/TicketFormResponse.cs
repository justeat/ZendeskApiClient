using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class TicketFormResponse
    {
        [JsonProperty("ticket_form")]
        public TicketForm Item { get; set; }
    }

    [JsonObject]
    public class TicketFormsResponse : PaginationResponse<TicketForm>
    {
        [JsonProperty("ticket_forms")]
        public override IEnumerable<TicketForm> Item { get; set; }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Responses
{
    [JsonObject]
    public class TicketFormsResponse : PaginationResponse<TicketForm>
    {
        [JsonProperty("ticket_forms")]
        public override IEnumerable<TicketForm> Item { get; set; }
    }
}

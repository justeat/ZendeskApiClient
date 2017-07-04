using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class TicketFormsResponse : PaginationResponse<TicketForm>
    {
        [JsonProperty("ticket_forms")]
        public IEnumerable<TicketForm> TicketForms { get; set; }

        protected override IEnumerable<TicketForm> Enumerable => TicketForms;
    }
}

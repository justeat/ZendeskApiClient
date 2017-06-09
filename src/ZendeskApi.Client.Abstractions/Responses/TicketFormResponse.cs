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

    public class TicketFormsResponse
    {
        [JsonProperty("ticket_forms")]
        public IEnumerable<TicketForm> Item { get; set; }
    }
}

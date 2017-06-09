using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class TicketFormsResponse
    {
        [JsonProperty("ticket_forms")]
        public IEnumerable<TicketForm> Item { get; set; }
    }
}

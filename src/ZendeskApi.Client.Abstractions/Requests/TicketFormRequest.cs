using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    public class TicketFormRequest
    {
        [JsonProperty("ticket_form")]
        public TicketForm Item { get; set; }
    }
}

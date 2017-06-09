using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    public class TicketFieldRequest
    {
        [JsonProperty("ticket_field")]
        public TicketField Item { get; set; }
    }
}

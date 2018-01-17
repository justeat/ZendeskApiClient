using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class TicketResponse
    {
        [JsonProperty("ticket")]
        public Ticket Ticket { get; set; }
    }
}
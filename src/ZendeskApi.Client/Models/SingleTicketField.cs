using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    public class SingleTicketField
    {
        [JsonProperty("ticket_field")]
        public TicketField TicketField { get; set; }
    }
}
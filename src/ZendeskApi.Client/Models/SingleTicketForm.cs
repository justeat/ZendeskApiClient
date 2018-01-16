using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    public class SingleTicketForm
    {
        [JsonProperty("ticket_form")]
        public TicketForm TicketForm { get; set; }
    }
}
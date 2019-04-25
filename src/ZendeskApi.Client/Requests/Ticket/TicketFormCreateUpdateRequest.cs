using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    public class TicketFormCreateUpdateRequest
    {
        public TicketFormCreateUpdateRequest(TicketForm ticketForm)
        {
            TicketForm = ticketForm;
        }

        [JsonProperty("ticket_form")]
        public TicketForm TicketForm { get; set; }
    }
}
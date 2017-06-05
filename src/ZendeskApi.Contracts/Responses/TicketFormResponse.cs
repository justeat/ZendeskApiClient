using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class TicketFormResponse : IResponse<TicketForm>
    {
        [JsonProperty("ticket_form")]
        public TicketForm Item { get; set; }
    }
}

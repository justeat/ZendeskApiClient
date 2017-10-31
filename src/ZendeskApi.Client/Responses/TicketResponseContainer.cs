using Newtonsoft.Json;

namespace ZendeskApi.Client.Responses
{
    public class TicketResponseContainer
    {
        [JsonProperty("ticket")]
        public TicketResponse Ticket { get; set; }
    }
}
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Tickets
{
    public class UpdateTicketRequest : BaseWriteTicket
    {
        public long Id { get; set; }

        [JsonProperty("requester_id")]
        public long? RequesterId { get; set; }
    }
}
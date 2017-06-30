using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Tickets
{
    public class CreateTicketRequest : BaseWriteTicket
    { 
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("requester_id")]
        public long RequesterId { get; set; }
    }
}
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class TicketCommentResponse
    {
        [JsonProperty("comment")]
        public TicketComment Item { get; set; }
    }
}

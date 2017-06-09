using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    public class TicketCommentRequest
    {
        [JsonProperty("comment")]
        public TicketComment Item { get; set; }
    }
}

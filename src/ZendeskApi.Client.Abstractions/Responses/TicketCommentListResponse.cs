using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class TicketCommentsResponse
    {
        [JsonProperty("comments")]
        public IEnumerable<TicketComment> Item { get; set; }
    }
}

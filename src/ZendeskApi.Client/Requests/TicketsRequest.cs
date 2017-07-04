using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Requests
{
    public class TicketsRequest
    {
        [JsonProperty("tickets")]
        public IEnumerable<TicketResponse> Item { get; set; }
    }
}

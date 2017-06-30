using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models.Tickets;

namespace ZendeskApi.Client.Requests
{
    public class TicketsRequest<T> where T : BaseWriteTicket
    {
        [JsonProperty("tickets")]
        public IEnumerable<T> Item { get; set; }
    }
}

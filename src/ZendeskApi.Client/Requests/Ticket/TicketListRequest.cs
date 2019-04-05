using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests
{
    internal class TicketListRequest<T>
    {
        public TicketListRequest(IEnumerable<T> tickets)
        {
            Tickets = tickets;
        }

        [JsonProperty("tickets")]
        public IEnumerable<T> Tickets { get; set; }
    }
}
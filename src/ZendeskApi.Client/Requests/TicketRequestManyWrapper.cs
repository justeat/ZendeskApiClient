using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests
{
    internal class TicketRequestManyWrapper<T>
    {
        public TicketRequestManyWrapper(IEnumerable<T> tickets)
        {
            Tickets = tickets;
        }

        [JsonProperty("tickets")]
        public IEnumerable<T> Tickets { get; set; }
    }
}
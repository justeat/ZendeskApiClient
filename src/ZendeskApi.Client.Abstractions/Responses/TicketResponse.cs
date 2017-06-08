using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class TicketResponse
    {
        [JsonProperty("ticket")]
        public Ticket Item { get; set; }
    }

    public class TicketsResponse
    {
        [JsonProperty("tickets")]
        public IEnumerable<Ticket> Item { get; set; }
    }

    public class JobStatusResponse
    {
        [JsonProperty("job_status")]
        public JobStatus Item { get; set; }
    }
}

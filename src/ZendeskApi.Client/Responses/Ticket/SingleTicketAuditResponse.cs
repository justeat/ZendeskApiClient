using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class SingleTicketAuditResponse
    {
        [JsonProperty("audit")]
        public TicketAudit Audit { get; set; }
    }
}
using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class TicketAuditResponse : CursorPaginationResponse<TicketAudit>
    {
        [JsonProperty("audits")]
        public IEnumerable<TicketAudit> Audits { get; set; }

        protected override IEnumerable<TicketAudit> Enumerable => Audits;
    }
}
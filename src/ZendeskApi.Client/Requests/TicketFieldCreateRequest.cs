using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    /// <summary>
    /// See: https://developer.zendesk.com/rest_api/docs/support/ticket_fields#create-ticket-field
    /// </summary>
    public class TicketFieldCreateRequest
    {
        public TicketFieldCreateRequest(TicketField ticketField)
        {
            TicketField = ticketField;
        }
        
        [JsonProperty("ticket_field")]
        public TicketField TicketField { get; set; }
    }
}
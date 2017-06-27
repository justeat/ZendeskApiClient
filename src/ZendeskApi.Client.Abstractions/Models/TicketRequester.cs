using System.ComponentModel;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    // partially documented under 
    // https://developer.zendesk.com/rest_api/docs/core/tickets#creating-a-ticket-with-a-new-requester
    [Description("Requester")]
    public class TicketRequester
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("locale_id")]
        public int? Locale { get; set; }
    }
}

using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ZendeskApi.Contracts.Models
{
    // partially documented under 
    // https://developer.zendesk.com/rest_api/docs/core/tickets#creating-a-ticket-with-a-new-requester
    [Description("Requester")]
    [DataContract]
    public class TicketRequester
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "locale_id")]
        public long? Locale { get; set; }
    }
}

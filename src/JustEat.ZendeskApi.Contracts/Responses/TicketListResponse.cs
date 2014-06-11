using System.Collections.Generic;
using System.Runtime.Serialization;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Responses
{
    [DataContract]
    internal class TicketListResponse : ListResponse<Ticket> 
    {
        [DataMember(Name = "tickets")]
        public override IEnumerable<Ticket> Results { get; set; }
    }
}

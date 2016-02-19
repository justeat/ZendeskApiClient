using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class TicketFieldResponse : IResponse<TicketField>
    {
        [DataMember(Name = "ticket_field")]
        public TicketField Item { get; set; }
    }
}

using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    [DataContract]
    public class TicketRequest : IRequest<Ticket>
    {
        [DataMember(Name = "ticket")]
        public Ticket Item { get; set; }
    }
}

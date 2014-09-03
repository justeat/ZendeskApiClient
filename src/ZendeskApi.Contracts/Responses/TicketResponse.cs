using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class TicketResponse : IResponse<Ticket>
    {
        [DataMember(Name = "ticket")]
        public Ticket Item { get; set; }
    }
}

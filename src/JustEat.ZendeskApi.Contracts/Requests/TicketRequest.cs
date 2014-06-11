using System.Runtime.Serialization;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Requests
{
    [DataContract]
    public class TicketRequest : IRequest<Ticket>
    {
        [DataMember(Name = "ticket")]
        public Ticket Entity { get; set; }
    }
}

using System.Runtime.Serialization;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class TicketResponse : IResponse<Ticket>
    {
        [DataMember(Name = "ticket")]
        public Ticket Item { get; set; }
    }
}

using System.Runtime.Serialization;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class TicketResponse 
    {
        [DataMember(Name = "ticket")]
        public Ticket Ticket { get; set; }
    }
}

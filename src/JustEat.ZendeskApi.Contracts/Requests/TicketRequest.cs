using System.Runtime.Serialization;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Requests
{
    [DataContract]
    public class TicketRequest 
    {
        [DataMember(Name = "ticket")]
        public Ticket Ticket { get; set; }
    }
}

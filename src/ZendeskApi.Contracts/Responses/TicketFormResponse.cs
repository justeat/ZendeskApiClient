using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class TicketFormResponse : IResponse<TicketForm>
    {
        [DataMember(Name = "ticket_form")]
        public TicketForm Item { get; set; }
    }
}

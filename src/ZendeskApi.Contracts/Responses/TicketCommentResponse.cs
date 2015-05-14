using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class TicketCommentResponse : IResponse<TicketComment>
    {
        [DataMember(Name = "comment")]
        public TicketComment Item { get; set; }
    }
}

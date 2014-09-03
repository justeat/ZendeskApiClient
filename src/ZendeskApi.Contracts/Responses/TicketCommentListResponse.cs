using System.Collections.Generic;
using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    internal class TicketCommentListResponse : ListResponse<TicketComment>
    {
        [DataMember(Name = "comments")]
        public override IEnumerable<TicketComment> Results { get; set; }
    }
}

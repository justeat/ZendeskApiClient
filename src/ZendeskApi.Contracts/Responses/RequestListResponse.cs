using System.Collections.Generic;
using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    internal class RequestListResponse : ListResponse<Request>
    {
        [DataMember(Name = "requests")]
        public override IEnumerable<Request> Results { get; set; }
    }
}

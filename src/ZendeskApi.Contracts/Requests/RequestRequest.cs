using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    [DataContract]
    public class RequestRequest : IRequest<Request>
    {
        [DataMember(Name = "request")]
        public Request Item { get; set; }
    }
}

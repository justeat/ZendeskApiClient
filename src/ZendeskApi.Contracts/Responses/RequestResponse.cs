using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class RequestResponse : IResponse<Request>
    {
        [DataMember(Name = "request")]
        public Request Item { get; set; }
    }
}

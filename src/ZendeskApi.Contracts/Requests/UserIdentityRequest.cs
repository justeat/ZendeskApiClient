using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    [DataContract]
    public class UserIdentityRequest : IRequest<UserIdentity>
    {
        [DataMember(Name = "identity")]
        public UserIdentity Item { get; set; }
    }
}

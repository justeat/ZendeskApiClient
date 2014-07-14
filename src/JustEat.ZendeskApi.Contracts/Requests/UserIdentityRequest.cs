using System.Runtime.Serialization;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Requests
{
    [DataContract]
    public class UserIdentityRequest : IRequest<UserIdentity>
    {
        [DataMember(Name = "identity")]
        public UserIdentity Item { get; set; }
    }
}

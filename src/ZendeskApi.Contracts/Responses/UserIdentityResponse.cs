using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    internal class UserIdentityResponse : IResponse<UserIdentity>
    {
        [DataMember(Name = "identity")]
        public UserIdentity Item { get; set; }
    }
}

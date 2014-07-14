using System.Collections.Generic;
using System.Runtime.Serialization;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Responses
{
    [DataContract]
    internal class UserIdentityResponse : IResponse<UserIdentity>
    {
        [DataMember(Name = "identity")]
        public UserIdentity Item { get; set; }
    }
}

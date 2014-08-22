using System.Collections.Generic;
using System.Runtime.Serialization;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Responses
{
    [DataContract]
    internal class UserIdentityListResponse : ListResponse<UserIdentity>
    {
        [DataMember(Name = "identities")]
        public override IEnumerable<UserIdentity> Results { get; set; }
    }
}

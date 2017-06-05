using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class UserIdentityListResponse : ListResponse<UserIdentity>
    {
        [JsonProperty("identities")]
        public override IEnumerable<UserIdentity> Results { get; set; }
    }
}

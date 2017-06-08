using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class UserIdentityListResponse : ListResponse<UserIdentity>
    {
        [JsonProperty("identities")]
        public override IEnumerable<UserIdentity> Results { get; set; }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class UserIdentityResponse
    {
        [JsonProperty("identity")]
        public UserIdentity Item { get; set; }
    }

    [JsonObject]
    public class UserIdentitiesResponse : PaginationResponse<UserIdentity>
    {
        [JsonProperty("identities")]
        public override IEnumerable<UserIdentity> Item { get; set; }
    }
}

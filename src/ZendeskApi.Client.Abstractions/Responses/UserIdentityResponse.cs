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

    public class UserIdentitiesResponse
    {
        [JsonProperty("identities")]
        public IEnumerable<UserIdentity> Item { get; set; }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class UserIdentitiesResponse : PaginationResponse<UserIdentity>
    {
        [JsonProperty("identities")]
        public override IEnumerable<UserIdentity> Item { get; set; }
    }
}

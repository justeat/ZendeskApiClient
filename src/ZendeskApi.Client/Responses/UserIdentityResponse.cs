using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Responses
{
    [JsonObject]
    public class UserIdentitiesResponse : PaginationResponse<UserIdentity>
    {
        [JsonProperty("identities")]
        public override IEnumerable<UserIdentity> Item { get; set; }
    }
}

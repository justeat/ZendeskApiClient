using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class UserIdentitiesResponse : PaginationResponse<UserIdentity>
    {
        [JsonProperty("identities")]
        public IEnumerable<UserIdentity> Identities { get; set; }


        protected override IEnumerable<UserIdentity> Enumerable => Identities;
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class UserIdentitiesResponse
    {
        [JsonProperty("identities")]
        public IEnumerable<UserIdentity> Item { get; set; }
    }
}

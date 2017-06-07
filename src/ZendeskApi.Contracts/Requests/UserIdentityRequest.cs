using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    public class UserIdentityRequest
    {
        [JsonProperty("identity")]
        public UserIdentity Item { get; set; }
    }
}

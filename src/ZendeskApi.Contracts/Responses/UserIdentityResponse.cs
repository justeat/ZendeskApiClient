using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class UserIdentityResponse
    {
        [JsonProperty("identity")]
        public UserIdentity Item { get; set; }
    }
}

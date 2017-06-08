using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class UserIdentityResponse
    {
        [JsonProperty("identity")]
        public UserIdentity Item { get; set; }
    }
}

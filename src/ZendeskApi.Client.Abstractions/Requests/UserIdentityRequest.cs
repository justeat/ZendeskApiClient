using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    public class UserIdentityRequest
    {
        [JsonProperty("identity")]
        public UserIdentity Item { get; set; }
    }
}

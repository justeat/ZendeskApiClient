using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class UserIdentityResponse : IResponse<UserIdentity>
    {
        [JsonProperty("identity")]
        public UserIdentity Item { get; set; }
    }
}

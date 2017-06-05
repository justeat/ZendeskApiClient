using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    public class UserIdentityRequest : IRequest<UserIdentity>
    {
        [JsonProperty("identity")]
        public UserIdentity Item { get; set; }
    }
}

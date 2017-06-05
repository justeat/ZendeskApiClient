using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    public class UserRequest : IRequest<User>
    {
        [JsonProperty("user")]
        public User Item { get; set; }
    }
}

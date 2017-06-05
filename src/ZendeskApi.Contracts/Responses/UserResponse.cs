using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class UserResponse : IResponse<User>
    {
        [JsonProperty("user")]
        public User Item { get; set; }
    }
}

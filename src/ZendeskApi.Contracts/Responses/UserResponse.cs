using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class UserResponse
    {
        [JsonProperty("user")]
        public User Item { get; set; }
    }
}

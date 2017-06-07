using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    public class UserRequest
    {
        [JsonProperty("user")]
        public User Item { get; set; }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class UserResponse
    {
        [JsonProperty("user")]
        public User Item { get; set; }
    }

    public class UsersResponse
    {
        [JsonProperty("users")]
        public IEnumerable<User> Item { get; set; }
    }
}

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

    [JsonObject]
    public class UsersResponse : PaginationResponse<User>
    {
        [JsonProperty("users")]
        public override IEnumerable<User> Item { get; set; }
    }
}

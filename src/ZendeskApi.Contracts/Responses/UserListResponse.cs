using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class UserListResponse : ListResponse<User>
    {
        [JsonProperty("users")]
        public override IEnumerable<User> Results { get; set; }
    }
}

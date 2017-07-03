using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Responses
{
    [JsonObject]
    public class UsersResponse : PaginationResponse<User>
    {
        [JsonProperty("users")]
        public override IEnumerable<User> Item { get; set; }
    }
}

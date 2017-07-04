using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class UsersResponse : PaginationResponse<User>
    {
        [JsonProperty("users")]
        public IEnumerable<User> Users { get; set; }
        
        protected override IEnumerable<User> Enumerable => Users;
    }
}

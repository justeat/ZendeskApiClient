using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class UsersListCursorResponse : CursorPaginationResponse<UserResponse>
    {
        [JsonProperty("users")]
        public IEnumerable<UserResponse> Users { get; set; }


        protected override IEnumerable<UserResponse> Enumerable => Users;
    }
}
using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class UserFieldsCursorResponse : CursorPaginationResponse<UserField>
    {
        [JsonProperty("user_fields")]
        public IEnumerable<UserField> UserFields { get; internal set; }

        protected override IEnumerable<UserField> Enumerable => UserFields;
    }
}

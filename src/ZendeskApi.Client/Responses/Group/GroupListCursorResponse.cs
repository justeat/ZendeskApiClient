using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class GroupListCursorResponse : CursorPaginationResponse<Group>
    {
        [JsonProperty("groups")]
        public IEnumerable<Group> Groups { get; set; }

        protected override IEnumerable<Group> Enumerable => Groups;
    }
}
using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class GroupResponse
    {
        [JsonProperty("group")]
        public Group Item { get; set; }
    }

    [JsonObject]
    public class GroupsResponse : PaginationResponse<Group>
    {
        [JsonProperty("groups")]
        public override IEnumerable<Group> Item { get; set; }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class GroupsResponse : PaginationResponse<Group>
    {
        [JsonProperty("groups")]
        public override IEnumerable<Group> Item { get; set; }
    }
}

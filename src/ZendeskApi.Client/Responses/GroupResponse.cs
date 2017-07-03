using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Responses
{
    [JsonObject]
    public class GroupsResponse : PaginationResponse<Group>
    {
        [JsonProperty("groups")]
        public override IEnumerable<Group> Item { get; set; }
    }
}

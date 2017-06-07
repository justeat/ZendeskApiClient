using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class GroupResponse
    {
        [JsonProperty("group")]
        public Group Item { get; set; }
    }

    public class GroupsResponse
    {
        [JsonProperty("groups")]
        public IEnumerable<Group> Item { get; set; }
    }
}

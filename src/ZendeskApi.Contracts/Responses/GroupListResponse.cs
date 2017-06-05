using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class GroupListResponse : ListResponse<Group> 
    {
        [JsonProperty("groups")]
        public override IEnumerable<Group> Results { get; set; }
    }
}

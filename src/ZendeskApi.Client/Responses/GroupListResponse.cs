using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class GroupListResponse : PaginationResponse<GroupResponse>
    {
        [JsonProperty("groups")]
        public IEnumerable<GroupResponse> Groups { get; set; }
        
        protected override IEnumerable<GroupResponse> Enumerable => Groups;
    }
}

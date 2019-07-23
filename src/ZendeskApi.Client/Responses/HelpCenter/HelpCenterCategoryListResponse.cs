using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class HelpCenterCategoryListResponse : PaginationResponse<HelpCenterCategory>
    {
        [JsonProperty("categories")]
        public IEnumerable<HelpCenterCategory> Groups { get; set; }

        protected override IEnumerable<HelpCenterCategory> Enumerable => Groups;
    }
}
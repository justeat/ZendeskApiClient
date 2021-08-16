using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class HelpCenterSectionListCursorResponse : PaginationResponse<HelpCenterSection>
    {
        [JsonProperty("sections")]
        public IEnumerable<HelpCenterSection> Sections { get; set; }

        protected override IEnumerable<HelpCenterSection> Enumerable => Sections;
    }
}
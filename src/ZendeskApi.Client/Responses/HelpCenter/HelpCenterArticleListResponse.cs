using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class HelpCenterArticleListResponse : PaginationResponse<HelpCenterArticle>
    {
        [JsonProperty("articles")]
        public IEnumerable<HelpCenterArticle> Articles { get; set; }

        protected override IEnumerable<HelpCenterArticle> Enumerable => Articles;
    }
}
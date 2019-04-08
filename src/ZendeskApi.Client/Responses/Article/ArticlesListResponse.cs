using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses.Articles
{
    [JsonObject]
    public class ArticlesListResponse : PaginationResponse<Article>
    {
        [JsonProperty("articles")]
        public IEnumerable<Article> Articles { get; set; }


        protected override IEnumerable<Article> Enumerable => Articles;

    }
}

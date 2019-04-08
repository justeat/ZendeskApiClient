using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests.Articles
{
    internal class ArticleListRequest<T>
    {
        public ArticleListRequest(IEnumerable<T> articles)
        {
            Articles = articles;
        }

        [JsonProperty("articles")]
        public IEnumerable<T> Articles { get; set; }
    }
}

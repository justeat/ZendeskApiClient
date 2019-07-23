using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class SingleHelpCenterArticleResponse
    {
        [JsonProperty("article")]
        public HelpCenterArticle Article { get; set; }
    }
}
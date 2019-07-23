using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class SingleHelpCenterCategoryResponse
    {
        [JsonProperty("category")]
        public HelpCenterCategory Category { get; set; }
    }
}
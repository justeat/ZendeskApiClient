using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class SingleHelpCenterSectionResponse
    {
        [JsonProperty("section")]
        public HelpCenterSection Section { get; set; }
    }
}
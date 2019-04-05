using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class OrganizationResponse
    {
        [JsonProperty("organization")]
        public Organization Organization { get; set; }
    }
}

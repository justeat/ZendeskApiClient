using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class OrganizationResponse
    {
        [JsonProperty("organization")]
        public Organization Item { get; set; }
    }
}

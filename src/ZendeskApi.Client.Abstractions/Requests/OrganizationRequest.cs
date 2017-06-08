using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    public class OrganizationRequest
    {
        [JsonProperty("organization")]
        public Organization Item { get; set; }
    }
}

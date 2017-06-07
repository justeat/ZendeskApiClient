using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    public class OrganizationRequest
    {
        [JsonProperty("organization")]
        public Organization Item { get; set; }
    }
}

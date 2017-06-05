using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class OrganizationResponse : IResponse<Organization>
    {
        [JsonProperty("organization")]
        public Organization Item { get; set; }
    }
}

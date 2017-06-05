using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    public class OrganizationRequest : IRequest<Organization>
    {
        [JsonProperty("organization")]
        public Organization Item { get; set; }
    }
}

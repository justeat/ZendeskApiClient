using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class OrganizationMembershipResponse : IResponse<OrganizationMembership>
    {
        [JsonProperty("organization_membership")]
        public OrganizationMembership Item { get; set; }
    }
}

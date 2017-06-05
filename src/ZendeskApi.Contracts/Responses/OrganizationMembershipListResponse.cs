using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class OrganizationMembershipListResponse : ListResponse<OrganizationMembership>
    {
        [JsonProperty("organization_memberships")]
        public override IEnumerable<OrganizationMembership> Results { get; set; }
    }
}

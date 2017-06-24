using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class OrganizationMembershipResponse
    {
        [JsonProperty("organization_membership")]
        public OrganizationMembership Item { get; set; }
    }

    [JsonObject]
    public class OrganizationMembershipsResponse : PaginationResponse<OrganizationMembership>
    {
        [JsonProperty("organization_memberships")]
        public override IEnumerable<OrganizationMembership> Item { get; set; }
    }
}

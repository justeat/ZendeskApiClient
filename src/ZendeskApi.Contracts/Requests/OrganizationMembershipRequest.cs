using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    public class OrganizationMembershipRequest
    {
        [JsonProperty("organization_membership")]
        public OrganizationMembership Item { get; set; }
    }

    public class OrganizationMembershipsRequest
    {
        [JsonProperty("organization_memberships")]
        public IEnumerable<OrganizationMembership> Item { get; set; }
    }
}

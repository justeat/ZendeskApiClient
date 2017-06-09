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

    public class OrganizationMembershipsResponse
    {
        [JsonProperty("organization_memberships")]
        public IEnumerable<OrganizationMembership> Item { get; set; }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class OrganizationMembershipListResponse : PaginationResponse<OrganizationMembership>
    {
        [JsonProperty("organization_memberships")]
        public IEnumerable<OrganizationMembership> OrganizationMemberships { get; set; }
        
        protected override IEnumerable<OrganizationMembership> Enumerable => OrganizationMemberships;
    }
}

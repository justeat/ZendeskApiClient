using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests
{
    internal class OrganizationMembershipsRequest
    {
        [JsonProperty("organization_memberships")]
        public IEnumerable<OrganizationMembershipCreateOperation> Item { get; set; }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    public class OrganizationMembershipListRequest
    {
        [JsonProperty("organization_memberships")]
        public IEnumerable<OrganizationMembership> Item { get; set; }
    }
}

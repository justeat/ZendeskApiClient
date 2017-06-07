using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class OrganizationMembershipsResponse
    {
        [JsonProperty("organization_memberships")]
        public IEnumerable<OrganizationMembership> Item { get; set; }
    }
}

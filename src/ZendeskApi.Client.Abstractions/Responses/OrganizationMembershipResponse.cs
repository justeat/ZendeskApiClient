using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class OrganizationMembershipsResponse : PaginationResponse<OrganizationMembership>
    {
        [JsonProperty("organization_memberships")]
        public override IEnumerable<OrganizationMembership> Item { get; set; }
    }
}

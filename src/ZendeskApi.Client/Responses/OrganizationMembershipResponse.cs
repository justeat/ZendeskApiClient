using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Responses
{
    [JsonObject]
    public class OrganizationMembershipsResponse : PaginationResponse<OrganizationMembership>
    {
        [JsonProperty("organization_memberships")]
        public override IEnumerable<OrganizationMembership> Item { get; set; }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class OrganizationResponse
    {
        [JsonProperty("organization")]
        public Organization Item { get; set; }
    }

    [JsonObject]
    public class OrganizationsResponse : PaginationResponse<Organization>
    {
        [JsonProperty("organizations")]
        public override IEnumerable<Organization> Item { get; set; }
    }
}

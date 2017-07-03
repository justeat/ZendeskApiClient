using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Responses
{
    [JsonObject]
    public class OrganizationsResponse : PaginationResponse<Organization>
    {
        [JsonProperty("organizations")]
        public override IEnumerable<Organization> Item { get; set; }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class OrganizationsResponse : PaginationResponse<Organization>
    {
        [JsonProperty("organizations")]
        public override IEnumerable<Organization> Item { get; set; }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class OrganizationListResponse : PaginationResponse<Organization>
    {
        [JsonProperty("organizations")]
        public IEnumerable<Organization> Organizations { get; set; }

        protected override IEnumerable<Organization> Enumerable => Organizations;
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class OrganizationsResponse : PaginationResponse<Organization>
    {
        [JsonProperty("organizations")]
        public IEnumerable<Organization> Organizations { get; internal set; }

        protected override IEnumerable<Organization> Enumerable => Organizations;
    }
}

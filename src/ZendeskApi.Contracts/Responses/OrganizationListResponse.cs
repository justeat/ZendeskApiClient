using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class OrganizationListResponse : ListResponse<Organization> 
    {
        [JsonProperty("organizations")]
        public override IEnumerable<Organization> Results { get; set; }
    }
}

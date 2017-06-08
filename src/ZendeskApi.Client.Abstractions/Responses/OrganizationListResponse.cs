using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class OrganizationListResponse : ListResponse<Organization> 
    {
        [JsonProperty("organizations")]
        public override IEnumerable<Organization> Results { get; set; }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class OrganizationsResponse
    {
        [JsonProperty("organizations")]
        public IEnumerable<Organization> Item { get; set; }
    }
}

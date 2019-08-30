using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests
{
    public class OrganizationListRequest<T>
    {
        public OrganizationListRequest(IEnumerable<T> organizations)
        {
            Organizations = organizations;
        }

        [JsonProperty("organizations")]
        public IEnumerable<T> Organizations { get; set; }
    }
}
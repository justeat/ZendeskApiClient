using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests
{
    public class OrganizationMembershipListRequest<T>
    {
        public OrganizationMembershipListRequest(IEnumerable<T> items)
        {
            Items = items;
        }

        [JsonProperty("organization_memberships")]
        public IEnumerable<T> Items { get; set; }
    }
}
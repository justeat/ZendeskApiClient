using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class OrganizationMembershipResponse
    {
        [JsonProperty("organization_membership")]
        public OrganizationMembership OrganizationMembership { get; set; }
    }
}
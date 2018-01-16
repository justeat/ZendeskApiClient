using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    public class SingleOrganizationMembership
    {
        [JsonProperty("organization_membership")]
        public OrganizationMembership OrganizationMembership { get; set; }
    }
}
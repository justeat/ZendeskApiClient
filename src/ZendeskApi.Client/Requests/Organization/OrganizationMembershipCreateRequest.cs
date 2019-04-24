using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    public class OrganizationMembershipCreateRequest
    {
        public OrganizationMembershipCreateRequest(OrganizationMembership membership)
        {
            OrganizationMembership = membership;
        }

        /// <summary>
        /// The OrganizationMembership to create
        /// </summary>
        [JsonProperty("organization_membership")]
        public OrganizationMembership OrganizationMembership { get; set; }
    }
}
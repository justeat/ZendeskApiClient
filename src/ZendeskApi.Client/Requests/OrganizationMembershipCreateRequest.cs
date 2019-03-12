using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    internal class OrganizationMembershipCreateRequest
    {
        /// <summary>
        /// The OrganizationMembership to create
        /// </summary>
        [JsonProperty("organization_membership")]
        public OrganizationMembership OrganizationMembership { get;}

        public OrganizationMembershipCreateRequest(OrganizationMembership membership)
        {
            OrganizationMembership = membership;
        }
    }
}
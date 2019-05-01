using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests
{
    internal class OrganizationMembershipCreateRequest
    {
        public OrganizationMembershipCreateRequest(OrganizationMembershipCreateOperation membership)
        {
            OrganizationMembership = membership;
        }

        /// <summary>
        /// The OrganizationMembership to create
        /// </summary>
        [JsonProperty("organization_membership")]
        public OrganizationMembershipCreateOperation OrganizationMembership { get; set; }
    }
}
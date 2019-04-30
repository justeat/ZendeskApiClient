using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    public class OrganizationMembershipCreateRequest
    {
        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("organization_id")]
        public long OrganizationId { get; set; }

        public OrganizationMembershipCreateRequest()
        { }

        public OrganizationMembershipCreateRequest(OrganizationMembership organizationMembership)
        {
            UserId = organizationMembership.UserId;
            OrganizationId = organizationMembership.OrganizationId;
        }
    }
}
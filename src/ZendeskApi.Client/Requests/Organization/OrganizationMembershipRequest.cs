using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests
{
    public class OrganizationMembershipRequest<T>
    {
        public OrganizationMembershipRequest(T organizationMembership)
        {
            OrganizationMembership = organizationMembership;
        }

        [JsonProperty("organization_membership")]
        public T OrganizationMembership { get; set; }
    }
}
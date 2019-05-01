using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests
{
    public interface IOrganizationMembershipCreateOperation
    {
        long? UserId { get; set; }
        long? OrganizationId { get; set; }
    }

    internal class OrganizationMembershipCreateOperation : IOrganizationMembershipCreateOperation
    {
        [JsonProperty("user_id")]
        public long? UserId { get; set; }

        [JsonProperty("organization_id")]
        public long? OrganizationId { get; set; }

        public OrganizationMembershipCreateOperation()
        { }

        public OrganizationMembershipCreateOperation(IOrganizationMembershipCreateOperation organizationMembershipCreateOperation)
        {
            UserId = organizationMembershipCreateOperation.UserId;
            OrganizationId = organizationMembershipCreateOperation.OrganizationId;
        }
    }
}

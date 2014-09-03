using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    internal class OrganizationMembershipResponse : IResponse<OrganizationMembership>
    {
        [DataMember(Name = "organization_membership")]
        public OrganizationMembership Item { get; set; }
    }
}

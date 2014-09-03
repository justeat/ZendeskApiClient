using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    [DataContract]
    public class OrganizationMembershipRequest : IRequest<OrganizationMembership>
    {
        [DataMember(Name = "organization_membership")]
        public OrganizationMembership Item { get; set; }
    }
}

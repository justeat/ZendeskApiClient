using System.Runtime.Serialization;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Requests
{
    [DataContract]
    public class OrganizationMembershipRequest : IRequest<OrganizationMembership>
    {
        [DataMember(Name = "organization_membership")]
        public OrganizationMembership Item { get; set; }
    }
}

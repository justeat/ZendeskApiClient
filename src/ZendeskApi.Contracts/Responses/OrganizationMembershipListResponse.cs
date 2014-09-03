using System.Collections.Generic;
using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    internal class OrganizationMembershipListResponse : ListResponse<OrganizationMembership>
    {
        [DataMember(Name = "organization_memberships")]
        public override IEnumerable<OrganizationMembership> Results { get; set; }
    }
}

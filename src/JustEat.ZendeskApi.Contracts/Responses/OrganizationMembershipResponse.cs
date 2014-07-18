using System.Collections.Generic;
using System.Runtime.Serialization;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Responses
{
    [DataContract]
    internal class OrganizationMembershipResponse : IResponse<OrganizationMembership>
    {
        [DataMember(Name = "organization_membership")]
        public OrganizationMembership Item { get; set; }
    }
}

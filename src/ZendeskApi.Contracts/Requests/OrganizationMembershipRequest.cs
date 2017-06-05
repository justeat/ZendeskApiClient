using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    public class OrganizationMembershipRequest : IRequest<OrganizationMembership>
    {
        [JsonProperty("organization_membership")]
        public OrganizationMembership Item { get; set; }
    }
}

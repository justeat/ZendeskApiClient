using System.Collections.Generic;
using System.Runtime.Serialization;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Responses
{
    [DataContract]
    internal class OrganizationListResponse : ListResponse<Organization> 
    {
        [DataMember(Name = "organizations")]
        public override IEnumerable<Organization> Results { get; set; }
    }
}

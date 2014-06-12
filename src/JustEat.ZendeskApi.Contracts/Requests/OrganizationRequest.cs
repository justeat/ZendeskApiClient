using System.Runtime.Serialization;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Requests
{
    [DataContract]
    public class OrganizationRequest : IRequest<Organization>
    {
        [DataMember(Name = "organization")]
        public Organization Item { get; set; }
    }
}

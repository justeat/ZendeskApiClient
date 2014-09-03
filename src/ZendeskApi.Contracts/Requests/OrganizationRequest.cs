using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    [DataContract]
    public class OrganizationRequest : IRequest<Organization>
    {
        [DataMember(Name = "organization")]
        public Organization Item { get; set; }
    }
}

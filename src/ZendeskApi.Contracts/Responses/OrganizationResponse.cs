using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class OrganizationResponse : IResponse<Organization>
    {
        [DataMember(Name = "organization")]
        public Organization Item { get; set; }
    }
}

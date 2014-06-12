using System.Runtime.Serialization;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class OrganizationResponse : IResponse<Organization>
    {
        [DataMember(Name = "organization")]
        public Organization Item { get; set; }
    }
}

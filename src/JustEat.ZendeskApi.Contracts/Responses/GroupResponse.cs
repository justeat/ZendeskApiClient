using System.Runtime.Serialization;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class GroupResponse : IResponse<Group>
    {
        [DataMember(Name = "group")]
        public Group Item { get; set; }
    }
}

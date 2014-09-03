using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class GroupResponse : IResponse<Group>
    {
        [DataMember(Name = "group")]
        public Group Item { get; set; }
    }
}

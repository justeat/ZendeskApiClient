using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    [DataContract]
    public class UserRequest : IRequest<User>
    {
        [DataMember(Name = "user")]
        public User Item { get; set; }
    }
}

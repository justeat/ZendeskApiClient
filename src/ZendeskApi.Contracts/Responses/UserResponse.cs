using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class UserResponse : IResponse<User>
    {
        [DataMember(Name = "user")]
        public User Item { get; set; }
    }
}

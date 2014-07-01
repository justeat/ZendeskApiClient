using System.Runtime.Serialization;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class UserResponse : IResponse<User>
    {
        [DataMember(Name = "user")]
        public User Item { get; set; }
    }
}

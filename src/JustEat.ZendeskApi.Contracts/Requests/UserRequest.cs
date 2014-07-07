using System.Runtime.Serialization;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Requests
{
    [DataContract]
    public class UserRequest : IRequest<User>
    {
        [DataMember(Name = "user")]
        public User Item { get; set; }
    }
}

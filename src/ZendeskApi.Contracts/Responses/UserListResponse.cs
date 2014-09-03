using System.Collections.Generic;
using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    internal class UserListResponse : ListResponse<User>
    {
        [DataMember(Name = "users")]
        public override IEnumerable<User> Results { get; set; }
    }
}

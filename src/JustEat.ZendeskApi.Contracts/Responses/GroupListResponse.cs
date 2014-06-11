using System.Collections.Generic;
using System.Runtime.Serialization;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Responses
{
    [DataContract]
    internal class GroupListResponse : ListResponse<Group> 
    {
        [DataMember(Name = "groups")]
        public override IEnumerable<Group> Results { get; set; }
    }
}

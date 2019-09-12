using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests.User
{
    internal class UserListRequest<T>
    {
        public UserListRequest(IEnumerable<T> users)
        {
            Users = users;
        }

        [JsonProperty("users")]
        public IEnumerable<T> Users { get; set; }
    }
}
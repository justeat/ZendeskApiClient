using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests
{
    internal class GroupListRequest<T>
    {
        [JsonProperty("groups")]
        public IEnumerable<T> Groups { get; set; }

        public GroupListRequest(IEnumerable<T> groups)
        {
            Groups = groups;
        }
    }
}
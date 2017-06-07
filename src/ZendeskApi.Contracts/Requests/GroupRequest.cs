using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    public class GroupRequest
    {
        [JsonProperty("group")]
        public Group Item { get; set; }
    }
}

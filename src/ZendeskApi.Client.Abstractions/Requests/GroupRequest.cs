using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    public class GroupRequest
    {
        [JsonProperty("group")]
        public Group Item { get; set; }
    }
}

using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class GroupResponse
    {
        [JsonProperty("group")]
        public Group Group { get; set; }
    }
}
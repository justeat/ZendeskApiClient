using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class GroupResponse
    {
        [JsonProperty]
        public Group Group { get; set; }
    }
}
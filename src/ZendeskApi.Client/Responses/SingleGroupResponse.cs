using Newtonsoft.Json;

namespace ZendeskApi.Client.Responses
{
    public class SingleGroupResponse
    {
        [JsonProperty]
        public GroupResponse Group { get; set; }
    }
}
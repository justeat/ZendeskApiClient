using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class GroupResponse : IResponse<Group>
    {
        [JsonProperty("group")]
        public Group Item { get; set; }
    }
}

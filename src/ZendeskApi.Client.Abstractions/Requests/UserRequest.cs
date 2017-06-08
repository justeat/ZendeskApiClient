using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    public class UserRequest
    {
        [JsonProperty("user")]
        public User Item { get; set; }
    }
}

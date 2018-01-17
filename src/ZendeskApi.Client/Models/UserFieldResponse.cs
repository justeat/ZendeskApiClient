using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    public class UserFieldResponse
    {
        [JsonProperty("user_field")]
        public UserField UserField { get; set; }
    }
}
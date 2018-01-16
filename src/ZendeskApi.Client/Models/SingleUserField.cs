using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    public class SingleUserField
    {
        [JsonProperty("user_field")]
        public UserField UserField { get; set; }
    }
}
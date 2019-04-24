using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests.User
{
    public class UserFieldCreateUpdateRequest
    {
        public UserFieldCreateUpdateRequest(UserField userField)
        {
            UserField = userField;
        }

        [JsonProperty("user_field")]
        public UserField UserField { get; set; }
    }
}

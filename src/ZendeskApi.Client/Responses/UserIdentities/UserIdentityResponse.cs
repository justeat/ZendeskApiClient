using Newtonsoft.Json;

namespace ZendeskApi.Client.Responses
{
    public class UserIdentityResponse<T>
    {
        [JsonProperty("identity")]
        public T Identity { get; set; }
    }
}
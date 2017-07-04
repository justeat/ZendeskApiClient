using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests
{
    internal class UserRequest<T>
    {
        [JsonProperty("user")]
        public T User { get; }

        public UserRequest(T user)
        {
            User = user;
        }
    }
}
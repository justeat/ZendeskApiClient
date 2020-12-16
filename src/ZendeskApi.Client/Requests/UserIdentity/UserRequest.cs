using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests
{
    internal class UserIdentityRequest<T>
    {
        [JsonProperty("identity")]
        public T Identity { get; }

        public UserIdentityRequest(T identity)
        {
            Identity = identity;
        }
    }
}
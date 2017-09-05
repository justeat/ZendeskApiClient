using System;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    [JsonObject("identity")]
    public class UserIdentity
    {
        [JsonProperty]
        public long? Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("user_id")]
        public long? UserId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("primary")]
        public bool Primary { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

    }

    [JsonObject("identity")]
    internal class IdentityWrapper
    {
        [JsonProperty("identity")]
        public UserIdentity Identity { get; set; }
    }

    internal class UserWrapper<T>
    {
        [JsonProperty("user")]
        public T User { get; set; }

        public UserWrapper(T user)
        {
            User = user;
        }
    }
}

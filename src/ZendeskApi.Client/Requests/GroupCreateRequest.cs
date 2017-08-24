using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests
{
    public class GroupCreateRequest
    {
        public GroupCreateRequest(string name)
        {
            Name = name;
        }

        /// <summary>
        /// The name of the group
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; }
    }
}
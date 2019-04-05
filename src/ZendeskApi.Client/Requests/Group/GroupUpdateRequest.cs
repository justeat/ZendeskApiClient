using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests
{
    public class GroupUpdateRequest
    {
        public GroupUpdateRequest(long id)
        {
            Id = id;
        }
        public long Id { get; set;  }

        /// <summary>
        /// The name of the group
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; }
    }
}
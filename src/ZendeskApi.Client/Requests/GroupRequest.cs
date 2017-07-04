using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests
{
    internal class GroupRequest<T>
    {
        [JsonProperty("group")]
        public T Group { get; set; }

        public GroupRequest(T group)
        {
            Group = group;
        }
    }
}
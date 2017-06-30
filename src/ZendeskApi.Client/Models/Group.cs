using System;
using System.Reflection;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    [JsonObject("group")]
    public class Group : ISearchResult
    {
        [JsonProperty]
        public long? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("has_incidents")]
        public bool HasIncidents { get; set; }

        DateTime ISearchResult.CreatedAt => CreatedAt.Value;
        DateTime ISearchResult.UpdatedAt => UpdatedAt.Value;
        long ISearchResult.Id => Id.Value;
        Uri ISearchResult.Url => Url;

        [JsonProperty("result_type")]
        string ISearchResult.Type => typeof(Group).GetTypeInfo().GetCustomAttribute<JsonObjectAttribute>().Id;
    }
}

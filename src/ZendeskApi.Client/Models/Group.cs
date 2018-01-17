using System;
using Newtonsoft.Json;
using ZendeskApi.Client.Converters;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Models
{
    /// <summary>
    /// <see href="https://developer.zendesk.com/rest_api/docs/core/groups#json-format">Group Response Format</see>
    /// </summary>
    [SearchResultType("group")]
    public class Group : ISearchResult
    {
        /// <summary>
        /// Automatically assigned when creating groups
        /// </summary>
        [JsonProperty]
        public long Id { get; set; }

        /// <summary>
        /// The API url of this group
        /// </summary>
        [JsonProperty("url")]
        public Uri Url { get; set; }

        /// <summary>
        /// The name of the group
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }      
        
        /// <summary>
        /// Deleted groups get marked as such
        /// </summary>
        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        /// <summary>
        /// The time the group was created
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The time of the last update of the group
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

       
        [JsonProperty("result_type")]
        string ResultType => "group";
    }
}

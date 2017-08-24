using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class SearchResponse<T> : PaginationResponse<T> where T : ISearchResult
    {
        [JsonProperty("results")]
        public IEnumerable<T> Results { get; set; }
        
        protected override IEnumerable<T> Enumerable => Results;
    }
}
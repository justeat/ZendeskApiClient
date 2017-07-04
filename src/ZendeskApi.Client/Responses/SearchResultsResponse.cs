using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class SearchResultsResponse : PaginationResponse<ISearchResult>
    {
        [JsonProperty("results")]
        public IEnumerable<ISearchResult> Results { get; set; }

        protected override IEnumerable<ISearchResult> Enumerable => Results;
    }

    [JsonObject]
    public class SearchResultsResponse<T> : PaginationResponse<T> where T : ISearchResult
    {
        [JsonProperty("results")]
        public IEnumerable<T> Results { get; set; }
        
        protected override IEnumerable<T> Enumerable => Results;
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class SearchResultsResponse : PaginationResponse<ISearchResult>
    {
        [JsonProperty("results")]
        public override IEnumerable<ISearchResult> Item { get; set; }
    }

    [JsonObject]
    public class SearchResultsResponse<T> : PaginationResponse<T> where T : ISearchResult
    {
        [JsonProperty("results")]
        public override IEnumerable<T> Item { get; set; }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Responses
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

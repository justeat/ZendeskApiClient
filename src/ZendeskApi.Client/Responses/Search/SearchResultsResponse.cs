using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class SearchResultsResponse : PaginationResponse<ISearchResult>
    {
        [JsonProperty("results")]
        public IEnumerable<ISearchResult> Results { get; set; }

        protected override IEnumerable<ISearchResult> Enumerable => Results;
    }
    
}

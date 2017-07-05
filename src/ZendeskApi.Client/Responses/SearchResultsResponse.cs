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
    
}

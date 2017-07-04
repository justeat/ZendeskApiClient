using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class SearchResultsResponse : PaginationResponse<ISearchResponse>
    {
        [JsonProperty("results")]
        public IEnumerable<ISearchResponse> Results { get; set; }

        protected override IEnumerable<ISearchResponse> Enumerable => Results;
    }
    
}

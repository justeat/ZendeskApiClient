using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class SearchResultsResponse : PaginationResponse<SearchResult>
    {
        [JsonProperty("results")]
        public override IEnumerable<SearchResult> Item { get; set; }
    }
}

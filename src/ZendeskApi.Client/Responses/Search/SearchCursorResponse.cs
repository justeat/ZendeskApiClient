using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class SearchCursorResponse<T> : CursorPaginationResponse<T> where T : ISearchResult
    {
        [JsonProperty("results")]
        public IEnumerable<T> Results { get; set; }
        
        protected override IEnumerable<T> Enumerable => Results;
    }
}
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Responses
{
    [JsonObject]
    public class RequestsResponse : PaginationResponse<Request>
    {
        [JsonProperty("requests")]
        public override IEnumerable<Request> Item { get; set; }
    }
}

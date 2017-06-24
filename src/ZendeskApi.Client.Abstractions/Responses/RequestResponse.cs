using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class RequestResponse
    {
        [JsonProperty("request")]
        public Request Item { get; set; }
    }

    [JsonObject]
    public class RequestsResponse : PaginationResponse<Request>
    {
        [JsonProperty("requests")]
        public override IEnumerable<Request> Item { get; set; }
    }
}

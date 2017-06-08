using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    internal class RequestListResponse : ListResponse<Request>
    {
        [JsonProperty("requests")]
        public override IEnumerable<Request> Results { get; set; }
    }
}

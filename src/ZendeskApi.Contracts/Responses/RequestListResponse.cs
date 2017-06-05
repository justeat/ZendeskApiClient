using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    internal class RequestListResponse : ListResponse<Request>
    {
        [JsonProperty("requests")]
        public override IEnumerable<Request> Results { get; set; }
    }
}

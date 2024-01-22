using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class RequestsCursorResponse : CursorPaginationResponse<Request>
    {
        [JsonProperty("requests")]
        public IEnumerable<Request> Requests { get; set; }

        protected override IEnumerable<Request> Enumerable => Requests;
    }
}
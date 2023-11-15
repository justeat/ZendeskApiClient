using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class TagResponse : CursorPaginationResponse<Tag>
    { 
        [JsonProperty("tags")]
        public IEnumerable<Tag> Tags { get; set; }

        protected override IEnumerable<Tag> Enumerable => Tags;
    }
}
using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class SatisfactionRatingsResponse : PaginationResponse<SatisfactionRating>
    {
        [JsonProperty("satisfaction_ratings")]
        public override IEnumerable<SatisfactionRating> Item { get; set; }
    }
}

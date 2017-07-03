using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Responses
{
    [JsonObject]
    public class SatisfactionRatingsResponse : PaginationResponse<SatisfactionRating>
    {
        [JsonProperty("satisfaction_ratings")]
        public override IEnumerable<SatisfactionRating> Item { get; set; }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class SatisfactionRatingsCursorResponse : CursorPaginationResponse<SatisfactionRating>
    {
        [JsonProperty("satisfaction_ratings")]
        public IEnumerable<SatisfactionRating> SatisfactionRatings { get; set; }

        protected override IEnumerable<SatisfactionRating> Enumerable => SatisfactionRatings;
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class SatisfactionRatingResponse
    {
        [JsonProperty("satisfaction_rating")]
        public SatisfactionRating Item { get; set; }
    }

    public class SatisfactionRatingsResponse
    {
        [JsonProperty("satisfaction_ratings")]
        public IEnumerable<SatisfactionRating> Item { get; set; }
    }
}

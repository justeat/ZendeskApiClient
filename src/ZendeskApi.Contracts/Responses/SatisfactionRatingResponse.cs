using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class SatisfactionRatingResponse
    {
        [JsonProperty("satisfaction_rating")]
        public SatisfactionRating Item { get; set; }
    }
}

using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    public class SatisfactionRatingRequest
    {
        [JsonProperty("satisfaction_rating")]
        public SatisfactionRating Item { get; set; }
    }
}

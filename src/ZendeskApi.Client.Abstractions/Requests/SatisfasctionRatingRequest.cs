using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    public class SatisfactionRatingRequest
    {
        [JsonProperty("satisfaction_rating")]
        public SatisfactionRating Item { get; set; }
    }
}

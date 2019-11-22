using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class SatisfactionRatingResponse
    {
        [JsonProperty("satisfaction_rating")]
        public SatisfactionRating SatisfactionRating { get; set; }
    }
}
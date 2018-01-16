using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    public class SingleSatisfactionRating
    {
        [JsonProperty("satisfaction_rating")]
        public SatisfactionRating SatisfactionRating { get; set; }
    }
}
using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    public class SatisfactionRatingRequest : IRequest<SatisfactionRating>
    {
        [JsonProperty("satisfaction_rating")]
        public SatisfactionRating Item { get; set; }
    }
}

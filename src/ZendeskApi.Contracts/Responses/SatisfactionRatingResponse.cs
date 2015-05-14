using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class SatisfactionRatingResponse : IResponse<SatisfactionRating>
    {
        [DataMember(Name = "satisfaction_rating")]
        public SatisfactionRating Item { get; set; }
    }
}

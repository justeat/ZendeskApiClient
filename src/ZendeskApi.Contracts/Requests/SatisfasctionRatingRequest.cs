using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    [DataContract]
    public class SatisfactionRatingRequest : IRequest<SatisfactionRating>
    {
        [DataMember(Name = "satisfaction_rating")]
        public SatisfactionRating Item { get; set; }
    }
}

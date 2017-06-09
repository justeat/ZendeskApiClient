using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Resources
{
    public interface ISatisfactionRatingsResource
    {
        Task<IEnumerable<SatisfactionRating>> GetAllAsync();
        Task<SatisfactionRating> GetAsync(long satisficationRatingId);
        Task<SatisfactionRating> CreateSatisfactionRatingAsync(SatisfactionRating satisfactionRating, long ticketId);
    }
}
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ISatisfactionRatingsResource
    {
        Task<IPagination<SatisfactionRating>> GetAllAsync();
        Task<SatisfactionRating> GetAsync(long satisficationRatingId);
        Task<SatisfactionRating> CreateSatisfactionRatingAsync(SatisfactionRating satisfactionRating, long ticketId);
    }
}
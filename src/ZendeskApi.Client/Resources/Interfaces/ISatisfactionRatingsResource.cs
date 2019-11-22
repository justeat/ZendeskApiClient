using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ISatisfactionRatingsResource
    {
        Task<IPagination<SatisfactionRating>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<SatisfactionRating> GetAsync(
            long satisficationRatingId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<SatisfactionRating> CreateAsync(
            SatisfactionRating satisfactionRating, 
            long ticketId,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
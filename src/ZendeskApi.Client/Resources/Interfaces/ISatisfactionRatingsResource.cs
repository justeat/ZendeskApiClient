using System;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ISatisfactionRatingsResource
    {
        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        Task<IPagination<SatisfactionRating>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<ICursorPagination<SatisfactionRating>> GetAllAsync(
            CursorPager pager,
            CancellationToken cancellationToken = default);

        Task<SatisfactionRating> GetAsync(
            long satisficationRatingId,
            CancellationToken cancellationToken = default);

        Task<SatisfactionRating> CreateAsync(
            SatisfactionRating satisfactionRating, 
            long ticketId,
            CancellationToken cancellationToken = default);
    }
}
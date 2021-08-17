using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class SatisfactionRatingsResource : AbstractBaseResource<SatisfactionRatingsResource>,
        ISatisfactionRatingsResource
    {
        private const string ResourceUri = "api/v2/satisfaction_ratings";
        private const string PostResourceUrlFormat = "api/v2/tickets/{0}/satisfaction_rating";

        public SatisfactionRatingsResource(
            IZendeskApiClient apiClient,
            ILogger logger)
            : base(apiClient, logger, "satisfaction_ratings")
        { }

        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        public async Task<IPagination<SatisfactionRating>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<SatisfactionRatingsResponse>(
                ResourceUri,
                "list-satisfaction-ratings",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<SatisfactionRatingsCursorResponse> GetAllAsync(
            CursorPager pager,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<SatisfactionRatingsCursorResponse>(
                ResourceUri,
                "list-satisfaction-ratings",
                "GetAllAsync",
                pager,
                cancellationToken);
        }

        public async Task<SatisfactionRating> GetAsync(
            long satisficationRatingId,
            CancellationToken cancellationToken = default)
        {
            var response = await GetWithNotFoundCheckAsync<SatisfactionRatingResponse>(
                $"{ResourceUri}/{satisficationRatingId}",
                "show-satisfaction-rating",
                $"GetAsync({satisficationRatingId})",
                $"Satisfaction Rating {satisficationRatingId} not found",
                cancellationToken: cancellationToken);

            return response?
                .SatisfactionRating;
        }

        public async Task<SatisfactionRating> CreateAsync(
            SatisfactionRating satisfactionRating, 
            long ticketId,
            CancellationToken cancellationToken = default)
        {
            return await CreateAsync<SatisfactionRating, SatisfactionRating>(
                string.Format(PostResourceUrlFormat, ticketId),
                satisfactionRating,
                "create-a-satisfaction-rating",
                cancellationToken: cancellationToken
            );
        }
    }
}

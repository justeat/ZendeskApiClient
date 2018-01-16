using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class SatisfactionRatingsResource : ISatisfactionRatingsResource
    {
        private const string ResourceUri = "api/v2/satisfaction_ratings";
        private const string PostResourceUrlFormat = "api/v2/tickets/{0}/satisfaction_rating";

        private readonly IZendeskApiClient _apiClient;
        private readonly ILogger _logger;

        private Func<ILogger, string, IDisposable> _loggerScope =
            LoggerMessage.DefineScope<string>(typeof(SatisfactionRatingsResource).Name + ": {0}");

        public SatisfactionRatingsResource(IZendeskApiClient apiClient,
            ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IPagination<SatisfactionRating>> GetAllAsync(PagerParameters pager = null)
        {
            using (_loggerScope(_logger, "GetAllAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(ResourceUri, pager).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<SatisfactionRatingsResponse>();
            }
        }

        public async Task<SatisfactionRating> GetAsync(long satisficationRatingId)
        {
            using (_loggerScope(_logger, $"GetAsync({satisficationRatingId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync(satisficationRatingId.ToString()).ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Satisfaction Rating {0} not found", satisficationRatingId);
                    return null;
                }

                response.EnsureSuccessStatusCode();

                var single = await response.Content.ReadAsAsync<SingleSatisfactionRating>();
                return single.SatisfactionRating;
            }
        }

        public async Task<SatisfactionRating> CreateAsync(SatisfactionRating satisfactionRating, long ticketId)
        {
            using (_loggerScope(_logger, $"PostAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.PostAsJsonAsync(string.Format(PostResourceUrlFormat, ticketId), satisfactionRating).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 201 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/satisfaction_ratings#create-a-satisfaction-rating");
                }

                return await response.Content.ReadAsAsync<SatisfactionRating>();
            }
        }

    }
}

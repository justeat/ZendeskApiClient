using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class SatisfactionRatingResource : ISatisfactionRatingResource
    {
        private readonly IRestClient _client;

        public SatisfactionRatingResource(IRestClient client)
        {
            _client = client;
        }

        public IResponse<SatisfactionRating> Get(long id)
        {
            var requestUri = _client.BuildUri($"/api/v2/satisfaction_ratings/{id}");
            return _client.Get<SatisfactionRatingResponse>(requestUri);
        }

        public async Task<IResponse<SatisfactionRating>> GetAsync(long id)
        {
            var requestUri = _client.BuildUri($"/api/v2/satisfaction_ratings/{id}");
            return await _client.GetAsync<SatisfactionRatingResponse>(requestUri).ConfigureAwait(false);
        }

        public IResponse<SatisfactionRating> Post(SatisfactionRatingRequest request, long ticketId)
        {
            var requestUri = _client.BuildUri($"/api/v2/tickets/{ticketId}/satisfaction_rating");
            return _client.Post<SatisfactionRatingResponse>(requestUri, request);
        }

        public async Task<IResponse<SatisfactionRating>> PostAsync(SatisfactionRatingRequest request, long ticketId)
        {
            var requestUri = _client.BuildUri($"/api/v2/tickets/{ticketId}/satisfaction_rating");
            return await _client.PostAsync<SatisfactionRatingResponse>(requestUri, request).ConfigureAwait(false);
        }
    }
}

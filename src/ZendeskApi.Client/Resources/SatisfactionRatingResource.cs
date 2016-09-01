using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class SatisfactionRatingResource : ZendeskResource<SatisfactionRating>, ISatisfactionRatingResource
    {
        private string _resourceUrl;
        protected override string ResourceUri => _resourceUrl ?? @"/api/v2/tickets/{0}/satisfaction_rating";

        public SatisfactionRatingResource(IRestClient client)
        {
            Client = client;
        }

        public IResponse<SatisfactionRating> Get(long id)
        {
            return GetAsync(id).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async Task<IResponse<SatisfactionRating>> GetAsync(long id)
        {
            _resourceUrl = "/api/v2/satisfaction_ratings";

            return await GetAsync<SatisfactionRatingResponse>(id).ConfigureAwait(false);
        }

        public IResponse<SatisfactionRating> Post(SatisfactionRatingRequest request, long ticketId)
        {
            return PostAsync(request, ticketId).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async Task<IResponse<SatisfactionRating>> PostAsync(SatisfactionRatingRequest request, long ticketId)
        {
            return await PostAsync<SatisfactionRatingRequest, SatisfactionRatingResponse>(request, ticketId).ConfigureAwait(false);
        }
    }
}

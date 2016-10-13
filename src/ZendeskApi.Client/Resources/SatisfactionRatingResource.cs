using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources.ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class SatisfactionRatingResource : ZendeskResource<SatisfactionRating>, ISatisfactionRatingResource
    {
        private const string GetResourceUrl = "/api/v2/satisfaction_ratings/{0}";
        private const string PostResourceUrl = "/api/v2/tickets/{0}/satisfaction_rating";

        public SatisfactionRatingResource(IRestClient client)
        {
            Client = client;
        }

        public IResponse<SatisfactionRating> Get(long id)
        {
            string url = string.Format(GetResourceUrl, id);
            return Get<SatisfactionRatingResponse>(url);
        }

        public async Task<IResponse<SatisfactionRating>> GetAsync(long id)
        {
            string url = string.Format(GetResourceUrl, id);
            return await GetAsync<SatisfactionRatingResponse>(url).ConfigureAwait(false);
        }

        public IResponse<SatisfactionRating> Post(SatisfactionRatingRequest request, long ticketId)
        {
            string url = string.Format(PostResourceUrl, ticketId);
            return Post<SatisfactionRatingRequest, SatisfactionRatingResponse>(request, url);
        }

        public async Task<IResponse<SatisfactionRating>> PostAsync(SatisfactionRatingRequest request, long ticketId)
        {
            string url = string.Format(PostResourceUrl, ticketId);
            return await PostAsync<SatisfactionRatingRequest, SatisfactionRatingResponse>(request, url).ConfigureAwait(false);
        }
    }
}

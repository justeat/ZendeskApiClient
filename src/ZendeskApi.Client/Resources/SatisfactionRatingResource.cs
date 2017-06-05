using System.Threading.Tasks;
using ZendeskApi.Client.Options;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class SatisfactionRatingResource : ZendeskResource<SatisfactionRating>, ISatisfactionRatingResource
    {
        private const string GetResourceUrl = "/api/v2/satisfaction_ratings";
        private const string PostResourceUrl = "/api/v2/tickets/{0}/satisfaction_rating";

        public SatisfactionRatingResource(ZendeskOptions options) : base(options) { }

        public async Task<IResponse<SatisfactionRating>> GetAsync(long id)
        {
            using (var client = CreateZendeskClient(GetResourceUrl + "/"))
            {
                var response = await client.GetAsync(id.ToString()).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<SatisfactionRatingResponse>();
            }
        }

        public async Task<IResponse<SatisfactionRating>> PostAsync(SatisfactionRatingRequest request, long ticketId)
        {
            using (var client = CreateZendeskClient(GetResourceUrl + "/"))
            {
                var response = await client.PostAsJsonAsync(string.Format(PostResourceUrl, ticketId), request).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<SatisfactionRatingResponse>();
            }
        }
    }
}

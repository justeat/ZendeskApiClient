using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class SatisfactionRatingResource : ISatisfactionRatingResource
    {
        private const string GetResourceUrl = "api/v2/satisfaction_ratings";
        private const string PostResourceUrl = "api/v2/tickets/{0}/satisfaction_rating";
        private readonly IZendeskApiClient _apiClient;

        public SatisfactionRatingResource(IZendeskApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<SatisfactionRating> GetAsync(long id)
        {
            using (var client = _apiClient.CreateClient(GetResourceUrl))
            {
                var response = await client.GetAsync(id.ToString()).ConfigureAwait(false);
                return (await response.Content.ReadAsAsync<SatisfactionRatingResponse>()).Item;
            }
        }

        public async Task<SatisfactionRating> PostAsync(SatisfactionRatingRequest request, long ticketId)
        {
            using (var client = _apiClient.CreateClient(GetResourceUrl))
            {
                var response = await client.PostAsJsonAsync(string.Format(PostResourceUrl, ticketId), request).ConfigureAwait(false);
                return (await response.Content.ReadAsAsync<SatisfactionRatingResponse>()).Item;
            }
        }
    }
}

using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class SatisfactionRatingResource : ZendeskResource<SatisfactionRating>, ISatisfactionRatingResource
    {
        private string _resourceUrl;
        protected override string ResourceUri
        {
            get { return _resourceUrl ?? @"/api/v2/tickets/{0}/satisfaction_rating"; }
        }

        public SatisfactionRatingResource(IRestClient client)
        {
            Client = client;
        }

        public IResponse<SatisfactionRating> Get(long id)
        {
            _resourceUrl = "/api/v2/satisfaction_ratings";

            return Get<SatisfactionRatingResponse>(id);
        }

        public IResponse<SatisfactionRating> Post(SatisfactionRatingRequest request, long ticketId)
        {
            return Post<SatisfactionRatingRequest, SatisfactionRatingResponse>(request, ticketId);
        }
    }
}

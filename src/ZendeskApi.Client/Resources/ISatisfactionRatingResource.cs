using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ISatisfactionRatingResource
    {
        IResponse<SatisfactionRating> Get(long id);
        IResponse<SatisfactionRating> Post(SatisfactionRatingRequest request, long ticketId);
    }
}
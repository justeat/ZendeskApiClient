using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ISatisfactionRatingResource
    {
        IResponse<SatisfactionRating> Get(long id);
        Task<IResponse<SatisfactionRating>> GetAsync(long id);
        IResponse<SatisfactionRating> Post(SatisfactionRatingRequest request, long ticketId);
        Task<IResponse<SatisfactionRating>> PostAsync(SatisfactionRatingRequest request, long ticketId);
    }
}
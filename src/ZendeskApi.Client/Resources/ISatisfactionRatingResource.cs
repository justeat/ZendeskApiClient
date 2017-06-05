using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ISatisfactionRatingResource
    {
        Task<IResponse<SatisfactionRating>> GetAsync(long id);
        Task<IResponse<SatisfactionRating>> PostAsync(SatisfactionRatingRequest request, long ticketId);
    }
}
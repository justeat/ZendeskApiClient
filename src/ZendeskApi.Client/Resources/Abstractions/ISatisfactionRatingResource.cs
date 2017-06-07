using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;

namespace ZendeskApi.Client.Resources
{
    public interface ISatisfactionRatingResource
    {
        Task<SatisfactionRating> GetAsync(long id);
        Task<SatisfactionRating> PostAsync(SatisfactionRatingRequest request, long ticketId);
    }
}
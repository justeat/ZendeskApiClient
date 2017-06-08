using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;

namespace ZendeskApi.Client.Resources
{
    public interface ISatisfactionRatingResource
    {
        Task<SatisfactionRating> GetAsync(long id);
        Task<SatisfactionRating> PostAsync(SatisfactionRatingRequest request, long ticketId);
    }
}
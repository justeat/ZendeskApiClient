using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;

namespace ZendeskApi.Client.Resources
{
    public interface IRequestResource
    {
        Task<Request> GetAsync(long id);
        Task<Request> GetAsync(IEnumerable<TicketStatus> requestedStatuses);
        Task<Request> PutAsync(RequestRequest request);
        Task<Request> PostAsync(RequestRequest request);
        Task DeleteAsync(long id);
    }
}
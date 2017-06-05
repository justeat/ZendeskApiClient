using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IRequestResource
    {
        Task<IResponse<Request>> GetAsync(long id);
        Task<IResponse<Request>> GetAsync(IEnumerable<TicketStatus> requestedStatuses);
        Task<IResponse<Request>> PutAsync(RequestRequest request);
        Task<IResponse<Request>> PostAsync(RequestRequest request);
        Task DeleteAsync(long id);
    }
}
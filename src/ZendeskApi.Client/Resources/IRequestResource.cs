using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IRequestResource
    {
        IResponse<Request> Get(long id);
        Task<IResponse<Request>> GetAsync(long id);
        IResponse<Request> Get(IEnumerable<TicketStatus> requestedStatuses);
        Task<IResponse<Request>> GetAsync(IEnumerable<TicketStatus> requestedStatuses);
        IResponse<Request> Put(RequestRequest request);
        Task<IResponse<Request>> PutAsync(RequestRequest request);
        IResponse<Request> Post(RequestRequest request);
        Task<IResponse<Request>> PostAsync(RequestRequest request);
        void Delete(long id);
        Task DeleteAsync(long id);

    }
}
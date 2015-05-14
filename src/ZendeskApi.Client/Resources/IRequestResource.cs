using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using System.Collections.Generic;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IRequestResource
    {
        IResponse<Request> Get(long id);
        IResponse<Request> Get(IEnumerable<TicketStatus> requestedStatuses);
        IResponse<Request> Put(RequestRequest request);
        IResponse<Request> Post(RequestRequest request);
    }
}
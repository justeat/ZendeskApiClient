using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IRequestResource
    {
        IResponse<Request> Get(long id);
        IResponse<Request> Put(RequestRequest request);
        IResponse<Request> Post(RequestRequest request);
    }
}
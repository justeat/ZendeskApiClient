using System.Collections.Generic;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IUserResource
    {
        IResponse<User> Get(long id);
        IListResponse<User> GetAll(List<long> ids);
        IResponse<User> Post(UserRequest request);
        IResponse<User> Put(UserRequest request);
        void Delete(long id);
    }
}
using System.Collections.Generic;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
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
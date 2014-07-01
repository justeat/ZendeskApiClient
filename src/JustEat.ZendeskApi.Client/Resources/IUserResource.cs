using System.Collections.Generic;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public interface IUserResource
    {
        IResponse<User> Get(long id);
        IListResponse<User> GetAll(List<long> ids);
    }
}
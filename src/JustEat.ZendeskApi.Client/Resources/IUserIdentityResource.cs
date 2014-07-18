using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public interface IUserIdentityResource
    {
        IListResponse<UserIdentity> GetAll(long id);
        IResponse<UserIdentity> Post(UserIdentityRequest request);
        IResponse<UserIdentity> Put(UserIdentityRequest request);
    }
}
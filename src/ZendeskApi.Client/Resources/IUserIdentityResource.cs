using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IUserIdentityResource
    {
        IListResponse<UserIdentity> GetAll(long id);
        IResponse<UserIdentity> Post(UserIdentityRequest request);
        IResponse<UserIdentity> Put(UserIdentityRequest request);
    }
}
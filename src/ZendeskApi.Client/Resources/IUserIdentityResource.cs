using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IUserIdentityResource
    {
        IListResponse<UserIdentity> GetAll(long id);
        Task<IListResponse<UserIdentity>> GetAllAsync(long id);
        IResponse<UserIdentity> Post(UserIdentityRequest request);
        Task<IResponse<UserIdentity>> PostAsync(UserIdentityRequest request);
        IResponse<UserIdentity> Put(UserIdentityRequest request);
        Task<IResponse<UserIdentity>> PutAsync(UserIdentityRequest request);
        void Delete(long id, long parentId);
        Task DeleteAsync(long id, long parentId);
    }
}
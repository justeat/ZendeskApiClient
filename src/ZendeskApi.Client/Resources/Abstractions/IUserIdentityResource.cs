using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IUserIdentityResource
    {
        Task<IListResponse<UserIdentity>> GetAllAsync(long id);
        Task<UserIdentity> PostAsync(UserIdentityRequest request);
        Task<UserIdentity> PutAsync(UserIdentityRequest request);
        Task DeleteAsync(long id, long parentId);
    }
}
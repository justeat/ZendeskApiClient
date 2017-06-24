using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IUserIdentityResource
    {
        Task<IPagination<UserIdentity>> GetAllForUserAsync(long userId);
        Task<UserIdentity> GetIdentityForUserAsync(long userId, long identityId);
        Task<UserIdentity> CreateUserIdentityAsync(UserIdentity identity, long userId);
        Task<UserIdentity> CreateEndUserIdentityAsync(UserIdentity identity, long endUserId);
        Task<UserIdentity> PutAsync(UserIdentity identity);
        Task DeleteAsync(long userId, long identityId);
    }
}
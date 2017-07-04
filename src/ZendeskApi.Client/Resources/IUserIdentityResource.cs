using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IUserIdentityResource
    {
        Task<IPagination<UserIdentity>> GetAllForUserAsync(long userId, PagerParameters pager = null);
        Task<UserIdentity> GetIdentityForUserAsync(long userId, long identityId);
        Task<UserIdentity> CreateUserIdentityAsync(UserIdentity identity, long userId);
        Task<UserIdentity> CreateEndUserIdentityAsync(UserIdentity identity, long endUserId);
        Task<UserIdentity> UpdateAsync(UserIdentity identity);
        Task DeleteAsync(long userId, long identityId);
    }
}
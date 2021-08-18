using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IUserIdentityResource
    {
        Task<IPagination<UserIdentity>> GetAllForUserAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<ICursorPagination<UserIdentity>> GetAllByUserIdAsync(
            long userId,
            CursorPager pager,
            CancellationToken cancellationToken = default);

        Task<UserIdentity> GetIdentityForUserAsync(
            long userId, 
            long identityId,
            CancellationToken cancellationToken = default);

        Task<UserIdentity> GetIdentityByUserIdAsync(
            long userId,
            long identityId,
            CancellationToken cancellationToken = default);

        Task<UserIdentity> CreateUserIdentityAsync(
            UserIdentity identity, 
            long userId,
            CancellationToken cancellationToken = default);

        Task<UserIdentity> CreateEndUserIdentityAsync(
            UserIdentity identity, 
            long endUserId,
            CancellationToken cancellationToken = default);

        Task<UserIdentity> UpdateAsync(
            UserIdentity identity,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            long userId, 
            long identityId,
            CancellationToken cancellationToken = default);
    }
}
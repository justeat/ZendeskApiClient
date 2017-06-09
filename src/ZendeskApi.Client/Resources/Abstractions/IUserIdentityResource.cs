using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Resources
{
    public interface IUserIdentityResource
    {
        Task<IEnumerable<UserIdentity>> GetAllForUserAsync(long userId);
        Task<UserIdentity> GetIdentifyForUserAsync(long userId, long identityId);
        Task<UserIdentity> CreateUserIdentityAsync(UserIdentity identity, long userId);
        Task<UserIdentity> CreateEndUserIdentityAsync(UserIdentity identity, long endUserId);
        Task<UserIdentity> PutAsync(UserIdentity identity);
        Task DeleteAsync(long userId, long identityId);
    }
}
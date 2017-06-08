using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Client.Resources
{
    public interface IUsersResource
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<IEnumerable<User>> GetAllUsersInGroupAsync(long groupId);
        Task<IEnumerable<User>> GetAllUsersInOrganizationAsync(long organizationId);
        Task<User> GetAsync(long userId);
        Task<IEnumerable<User>> GetAllAsync(long[] userIds);
        Task<IEnumerable<User>> GetAllByExternalIdsAsync(long[] externalIds);
        Task<User> GetRelatedUsersAsync(long userId);
        Task<User> PostAsync(User user);
        Task<User> PutAsync(User user);
        Task DeleteAsync(long userId);
    }
}
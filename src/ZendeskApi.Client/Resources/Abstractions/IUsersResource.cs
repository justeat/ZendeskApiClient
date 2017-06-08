using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;

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
        Task<User> PostAsync(UserRequest request);
        Task<User> PutAsync(UserRequest request);
        Task DeleteAsync(long userId);
    }
}
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IUsersResource
    {
        Task<IPagination<User>> GetAllAsync(PagerParameters pager = null);
        Task<IPagination<User>> GetAllUsersInGroupAsync(long groupId, PagerParameters pager = null);
        Task<IPagination<User>> GetAllUsersInOrganizationAsync(long organizationId, PagerParameters pager = null);
        Task<User> GetAsync(long userId);
        Task<IPagination<User>> GetAllAsync(long[] userIds, PagerParameters pager = null);
        Task<IPagination<User>> GetAllByExternalIdsAsync(string[] externalIds, PagerParameters pager = null);
        Task<User> GetRelatedUsersAsync(long userId);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task DeleteAsync(long userId);
    }
}
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IUsersResource
    {
        Task<IPagination<User>> GetAllAsync();
        Task<IPagination<User>> GetAllUsersInGroupAsync(long groupId);
        Task<IPagination<User>> GetAllUsersInOrganizationAsync(long organizationId);
        Task<User> GetAsync(long userId);
        Task<IPagination<User>> GetAllAsync(long[] userIds);
        Task<IPagination<User>> GetAllByExternalIdsAsync(string[] externalIds);
        Task<User> GetRelatedUsersAsync(long userId);
        Task<User> PostAsync(User user);
        Task<User> PutAsync(User user);
        Task DeleteAsync(long userId);
    }
}
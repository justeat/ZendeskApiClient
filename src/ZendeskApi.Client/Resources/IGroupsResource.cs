using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Models.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IGroupsResource
    {
        Task<IPagination<Group>> GetAllAsync(PagerParameters pager = null);
        Task<IPagination<Group>> GetAllAsync(long userId, PagerParameters pager = null);
        Task<IPagination<Group>> GetAllAssignableAsync(PagerParameters pager = null);
        Task<Group> GetAsync(long groupId);
        Task<Group> CreateAsync(Group group);
        Task<Group> UpdateAsync(Group group);
        Task DeleteAsync(long groupId);
    }
}
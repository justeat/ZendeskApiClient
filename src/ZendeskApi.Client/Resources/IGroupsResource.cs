using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IGroupsResource
    {
        Task<GroupListResponse> ListAsync(PagerParameters pager = null);
        Task<GroupListResponse> ListAsync(long userId, PagerParameters pager = null);
        Task<GroupListResponse> ListAssignableAsync(PagerParameters pager = null);
        Task<Group> GetAsync(long groupId);
        Task<Group> CreateAsync(GroupCreateRequest group);
        Task<Group> UpdateAsync(GroupUpdateRequest group);
        Task DeleteAsync(long groupId);
    }
}
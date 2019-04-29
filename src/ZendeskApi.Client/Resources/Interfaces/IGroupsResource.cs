using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IGroupsResource
    {
        Task<GroupListResponse> ListAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<GroupListResponse> ListAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<GroupListResponse> ListAssignableAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Group> GetAsync(
            long groupId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Group> CreateAsync(
            GroupCreateRequest group,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Group> UpdateAsync(
            GroupUpdateRequest group,
            CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteAsync(
            long groupId,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
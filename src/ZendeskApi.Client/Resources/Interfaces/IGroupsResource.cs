using System;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IGroupsResource
    {
        [Obsolete("Use `GetAllAsync` instead.")]
        Task<GroupListResponse> ListAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllByUserIdAsync` instead.")]
        Task<GroupListResponse> ListAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllByAssignableAsync` instead.")]
        Task<GroupListResponse> ListAssignableAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllAsyncWithCursor` instead.")]
        Task<GroupListResponse> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<GroupListCursorResponse> GetAllAsyncWithCursor(
            CursorPager pager = null,
            CancellationToken cancellationToken = default);

        Task<GroupListResponse> GetAllByUserIdAsync(
            long userId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllByAssignableAsync` instead.")]
        Task<GroupListResponse> GetAllByAssignableAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<GroupListCursorResponse> GetAllByAssignableAsyncWithCursor(
            CursorPager pager = null,
            CancellationToken cancellationToken = default);

        Task<Group> GetAsync(
            long groupId,
            CancellationToken cancellationToken = default);

        Task<Group> CreateAsync(
            GroupCreateRequest group,
            CancellationToken cancellationToken = default);

        Task<Group> UpdateAsync(
            GroupUpdateRequest group,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            long groupId,
            CancellationToken cancellationToken = default);
    }
}
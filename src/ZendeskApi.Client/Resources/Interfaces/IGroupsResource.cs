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

        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        Task<GroupListResponse> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<GroupListCursorResponse> GetAllAsync(
            CursorPager pager,
            CancellationToken cancellationToken = default);

        Task<GroupListResponse> GetAllByUserIdAsync(
            long userId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllByAssignableAsync` with CursorPager parameter instead.")]
        Task<GroupListResponse> GetAllByAssignableAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<GroupListCursorResponse> GetAllByAssignableAsync(
            CursorPager pager,
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
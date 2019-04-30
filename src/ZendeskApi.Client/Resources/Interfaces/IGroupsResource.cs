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
            CancellationToken cancellationToken = default(CancellationToken));

        [Obsolete("Use `GetAllByUserIdAsync` instead.")]
        Task<GroupListResponse> ListAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        [Obsolete("Use `GetAllByAssignableAsync` instead.")]
        Task<GroupListResponse> ListAssignableAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<GroupListResponse> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<GroupListResponse> GetAllByUserIdAsync(
            long userId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<GroupListResponse> GetAllByAssignableAsync(
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
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IUsersResource
    {
        #region List Users
        [Obsolete("Use `GetAllAsync` instead.")]
        Task<UsersListResponse> ListAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllByGroupIdAsync` instead.")]
        Task<UsersListResponse> ListInGroupAsync(
            long groupId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllByOrganizationIdAsync` instead.")]
        Task<UsersListResponse> ListInOrganizationAsync(
            long organizationId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllAsync` instead.")]
        Task<UsersListResponse> ListAsync(
            long[] userIds, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllByExternalIdsAsync` instead.")]
        Task<UsersListResponse> ListByExternalIdsAsync(
            string[] externalIds, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        #endregion

        #region Get Users
        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        Task<UsersListResponse> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);
        
        Task<UsersListCursorResponse> GetAllAsync(
            CursorPager pager,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllByGroupIdAsync` with CursorPager parameter instead.")]
        Task<UsersListResponse> GetAllByGroupIdAsync(
            long groupId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<UsersListCursorResponse> GetAllByGroupIdAsync(
            long groupId,
            CursorPager pager,
            CancellationToken cancellationToken = default);
        
        [Obsolete("Use `GetAllByOrganizationIdAsync` with CursorPager parameter instead.")]
        Task<UsersListResponse> GetAllByOrganizationIdAsync(
            long organizationId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<UsersListCursorResponse> GetAllByOrganizationIdAsync(
            long organizationId,
            CursorPager pager,
            CancellationToken cancellationToken = default);

        Task<UsersListResponse> GetAllByExternalIdsAsync(
            string[] externalIds,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<UserResponse> GetAsync(
            long userId,
            CancellationToken cancellationToken = default);

        Task<UsersListResponse> GetAllAsync(
            long[] userIds,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<UserRelatedInformationResponse> GetRelatedInformationAsync(
            long userId,
            CancellationToken cancellationToken = default);

        Task<IncrementalUsersResponse<UserResponse>> GetIncrementalExport(
            DateTime startTime,
            CancellationToken cancellationToken = default);
        #endregion

        #region Create Users
        Task<UserResponse> CreateAsync(
            UserCreateRequest user,
            CancellationToken cancellationToken = default);
        #endregion

        #region Update Users
        Task<UserResponse> UpdateAsync(
            UserUpdateRequest user,
            CancellationToken cancellationToken = default);

        Task<JobStatusResponse> UpdateAsync(IEnumerable<UserUpdateRequest> users,
            CancellationToken cancellationToken = default);
        #endregion

        #region Delete Users
        Task DeleteAsync(
            long userId,
            CancellationToken cancellationToken = default);

        Task<JobStatusResponse> DeleteAsync(
            IEnumerable<long> userIds,
            CancellationToken cancellationToken = default);

        #endregion
    }
}
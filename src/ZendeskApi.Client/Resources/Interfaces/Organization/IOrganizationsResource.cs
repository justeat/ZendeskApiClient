using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IOrganizationsResource
    {
        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        Task<IPagination<Organization>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<ICursorPagination<Organization>> GetAllAsync(
            CursorPager pager,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllByUserIdAsync` with CursorPager parameter instead.")]
        Task<IPagination<Organization>> GetAllByUserIdAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<ICursorPagination<Organization>> GetAllByUserIdAsync(
            long userId,
            CursorPager pager,
            CancellationToken cancellationToken = default);

        Task<Organization> GetAsync(
            long organizationId,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        Task<IPagination<Organization>> GetAllAsync(
            long[] organizationIds, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<ICursorPagination<Organization>> GetAllByOrganizationIdsAsync(
            long[] organizationIds,
            CursorPager pager,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllByExternalIdsAsync` with CursorPager parameter instead.")]
        Task<IPagination<Organization>> GetAllByExternalIdsAsync(
            string[] externalIds, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<ICursorPagination<Organization>> GetAllByExternalIdsAsync(
            string[] externalIds,
            CursorPager pager,
            CancellationToken cancellationToken = default);


        Task<Organization> CreateAsync(
            Organization organization,
            CancellationToken cancellationToken = default);

        Task<Organization> UpdateAsync(
            Organization organization,
            CancellationToken cancellationToken = default);

        Task<JobStatusResponse> UpdateAsync(
            IEnumerable<Organization> organizations,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            long organizationId,
            CancellationToken cancellationToken = default);
    }
}
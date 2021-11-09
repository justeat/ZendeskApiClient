using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IOrganizationMembershipsResource
    {
        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        Task<IPagination<OrganizationMembership>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<ICursorPagination<OrganizationMembership>> GetAllAsync(
            CursorPager pager,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllByOrganizationIdAsync` with CursorPager parameter instead.")]
        Task<IPagination<OrganizationMembership>> GetAllForOrganizationAsync(
            long organizationId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllByOrganizationIdAsync` with CursorPager parameter instead.")]
        Task<IPagination<OrganizationMembership>> GetAllByOrganizationIdAsync(
            long organizationId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<ICursorPagination<OrganizationMembership>> GetAllByOrganizationIdAsync(
            long organizationId,
            CursorPager pager,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllByUserIdAsync` with CursorPager parameter instead.")]
        Task<IPagination<OrganizationMembership>> GetAllForUserAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllByUserIdAsync` with CursorPager parameter instead.")]
        Task<IPagination<OrganizationMembership>> GetAllByUserIdAsync(
            long userId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<ICursorPagination<OrganizationMembership>> GetAllByUserIdAsync(
            long userId,
            CursorPager pager = null,
            CancellationToken cancellationToken = default);

        Task<OrganizationMembership> GetAsync(
            long id,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetByUserIdAndOrganizationIdAsync` instead.")]
        Task<OrganizationMembership> GetForUserAndOrganizationAsync(
            long userId, 
            long organizationId,
            CancellationToken cancellationToken = default);

        Task<OrganizationMembership> GetByUserIdAndOrganizationIdAsync(
            long userId,
            long organizationId,
            CancellationToken cancellationToken = default);

        Task<OrganizationMembership> CreateAsync(
            OrganizationMembership organizationMembership,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `PostByUserIdAsync` instead.")]
        Task<OrganizationMembership> PostForUserAsync(
            OrganizationMembership organizationMembership, 
            long userId,
            CancellationToken cancellationToken = default);

        Task<OrganizationMembership> PostByUserIdAsync(
            OrganizationMembership organizationMembership,
            long userId,
            CancellationToken cancellationToken = default);

        Task<JobStatusResponse> CreateAsync(
            IEnumerable<OrganizationMembership> organizationMemberships,
            CancellationToken cancellationToken = default);

        Task<IPagination<OrganizationMembership>> MakeDefault(
            long userId, 
            long organizationMembershipId,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            long organizationMembershipId,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `DeleteByUserIdAsync` instead.")]
        Task DeleteAsync(
            long userId, 
            long organizationMembershipId,
            CancellationToken cancellationToken = default);

        Task DeleteByUserIdAsync(
            long userId,
            long organizationMembershipId,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            IEnumerable<long> organizationMembershipIds,
            CancellationToken cancellationToken = default);
    }
}
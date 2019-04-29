using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IOrganizationsResource
    {
        Task<IPagination<Organization>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<IPagination<Organization>> GetAllByUserIdAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Organization> GetAsync(
            long organizationId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<IPagination<Organization>> GetAllAsync(
            long[] organizationIds, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<IPagination<Organization>> GetAllByExternalIdsAsync(
            string[] externalIds, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Organization> CreateAsync(
            Organization organization,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Organization> UpdateAsync(
            Organization organization,
            CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteAsync(
            long organizationId,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
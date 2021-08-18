using System;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IOrganizationFieldsResource
    {
        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        Task<IPagination<OrganizationField>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<ICursorPagination<OrganizationField>> GetAllAsync(
            CursorPager pager,
            CancellationToken cancellationToken = default);

        Task<OrganizationField> GetAsync(
            long orgFieldId,
            CancellationToken cancellationToken = default);

        Task<OrganizationField> CreateAsync(
            OrganizationField organizationField,
            CancellationToken cancellationToken = default);

        Task<OrganizationField> UpdateAsync(
            OrganizationField organizationField,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            long orgFieldId,
            CancellationToken cancellationToken = default);

        Task ReorderAsync(OrganizationFieldsReorderRequest request,
            CancellationToken cancellationToken = default);
    }
}
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IOrganizationFieldsResource
    {
        Task<IPagination<OrganizationField>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<OrganizationField> GetAsync(
            long orgFieldId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<OrganizationField> CreateAsync(
            OrganizationField organizationField,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<OrganizationField> UpdateAsync(
            OrganizationField organizationField,
            CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteAsync(
            long orgFieldId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task ReorderAsync(OrganizationFieldsReorderRequest request,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
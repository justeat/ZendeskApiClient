using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IUserFieldsResource
    {
        Task<IPagination<UserField>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<UserField> GetAsync(
            long userFieldId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<UserField> CreateAsync(
            UserField userField,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<UserField> UpdateAsync(
            UserField userField,
            CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteAsync(
            long userFieldId,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}

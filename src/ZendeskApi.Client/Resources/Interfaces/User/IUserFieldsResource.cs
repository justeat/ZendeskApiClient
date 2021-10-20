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
            CancellationToken cancellationToken = default);

        Task<ICursorPagination<UserField>> GetAllAsync(
            CursorPager pager,
            CancellationToken cancellationToken = default);

        Task<UserField> GetAsync(
            long userFieldId,
            CancellationToken cancellationToken = default);

        Task<UserField> CreateAsync(
            UserField userField,
            CancellationToken cancellationToken = default);

        Task<UserField> UpdateAsync(
            UserField userField,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            long userFieldId,
            CancellationToken cancellationToken = default);
    }
}

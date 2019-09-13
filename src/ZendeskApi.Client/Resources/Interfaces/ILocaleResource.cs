using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Resources.Interfaces
{
    public interface ILocaleResource
    {
        Task<IReadOnlyList<Locale>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<IReadOnlyList<Locale>> GetAllPublicAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<IReadOnlyList<Locale>> GetAllAgentAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<Locale> GetAsync(
            long id,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Locale> GetCurrentAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<Locale> GetBestLanguageForUserAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}

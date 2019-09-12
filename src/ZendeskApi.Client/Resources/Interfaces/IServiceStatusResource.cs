using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Resources.Interfaces
{
    public interface IServiceStatusResource
    {
        Task<IReadOnlyList<Component>> ListComponents(
            string subdomain,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<IReadOnlyList<SubComponent>> ListSubComponents(
            string componentIdOrTag, 
            string subdomain,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<ServiceStatus> GetComponentStatus(
            string componentIdOrTag, 
            string subdomain,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<ServiceStatus> GetSubComponentStatus(
            string componentIdOrTag, 
            string subComponentIdOrTag, 
            string subdomain,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}

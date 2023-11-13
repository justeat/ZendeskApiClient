using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ZendeskApi.Client.Resources.Interfaces
{
    public interface IPaginatedResource
    {
        Task<HttpResponseMessage> GetPage(
             string url,
             CancellationToken cancellationToken = default);
    }
}


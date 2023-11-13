using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Resources.Interfaces;

namespace ZendeskApi.Client.Resources
{
    public class PaginatedResource : AbstractBaseResource<PaginatedResource>, IPaginatedResource
    {
        public PaginatedResource(
            IZendeskApiClient apiClient,
            ILogger logger)
            : base(apiClient, logger, "")
        { }

        public async Task<HttpResponseMessage> GetPage(string url, CancellationToken cancellationToken = default)
        {
            using var client = ApiClient.CreateClient();
            return await client.GetAsync(url);
        }
    }
}


using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources.Interfaces;

namespace ZendeskApi.Client.Resources
{
    public class HelpCenterLocalesResource : AbstractBaseResource<HelpCenterLocalesResource>,
        IHelpCenterLocalesResource
    {
        private const string ResourceUri = "api/v2/help_center/locales";

        public HelpCenterLocalesResource(
            IZendeskApiClient apiClient, 
            ILogger logger) 
            : base(apiClient, logger, "help_center/locales")
        { }

        public async Task<HelpCenterLocales> GetAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await ExecuteRequest(async (client, token) =>
                        await client.GetAsync(ResourceUri, token).ConfigureAwait(false),
                    "GetAsync",
                    cancellationToken)
                .ThrowIfUnsuccessful("list-locales")
                .ReadContentAsAsync<HelpCenterLocales>();

            return response;
        }
    }
}
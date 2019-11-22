using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources.Interfaces;

namespace ZendeskApi.Client.Resources
{
    public class ServiceStatusResource : IServiceStatusResource
    {
        private readonly IZendeskApiClient _httpClientFactory;
        private readonly ILogger _logger;

        private readonly Func<ILogger, string, IDisposable> _loggerScope = LoggerMessage.DefineScope<string>(typeof(ServiceStatusResource).Name + ": {0}");

        public ServiceStatusResource(
            IZendeskApiClient httpClientFactory,
            ILogger logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Component>> ListComponents(
            string subdomain,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await ExecuteRequest(async (client, token) =>
                        await client.GetAsync($"/api/components?domain={subdomain}.zendesk.com", token).ConfigureAwait(false),
                    "ListComponents",
                    cancellationToken)
                .ThrowIfUnsuccessful("status_api#list-components", helpDocLinkPrefix: "status-api")
                .ReadContentAsAsync<ComponentResponse>();

            return response.Components;
        }

        public async Task<IReadOnlyList<SubComponent>> ListSubComponents(
            string componentIdOrTag, 
            string subdomain,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await ExecuteRequest(async (client, token) =>
                        await client.GetAsync($"/api/components/{componentIdOrTag}/subcomponents?domain={subdomain}.zendesk.com", token).ConfigureAwait(false),
                    "ListSubComponents",
                    cancellationToken)
                .ThrowIfUnsuccessful("status_api#list-subcomponents", helpDocLinkPrefix: "status-api")
                .ReadContentAsAsync<SubComponentResponse>();

            return response.SubComponents;
        }

        public async Task<ServiceStatus> GetComponentStatus(
            string componentIdOrTag, 
            string subdomain,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await ExecuteRequest(async (client, token) =>
                        await client.GetAsync($"/api/components/{componentIdOrTag}?domain={subdomain}.zendesk.com", token).ConfigureAwait(false),
                    "GetComponentStatus",
                    cancellationToken)
                .ThrowIfUnsuccessful("status_api#show-status-of-a-component", helpDocLinkPrefix: "status-api")
                .ReadContentAsAsync<ServiceStatus>();
        }

        public async Task<ServiceStatus> GetSubComponentStatus(
            string componentIdOrTag, 
            string subComponentIdOrTag, 
            string subdomain,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await ExecuteRequest(async (client, token) =>
                        await client.GetAsync($"/api/components/{componentIdOrTag}/subcomponents/{subComponentIdOrTag}?domain={subdomain}.zendesk.com", token).ConfigureAwait(false),
                    "GetSubComponentStatus",
                    cancellationToken)
                .ThrowIfUnsuccessful("status_api#show-status-of-a-subcomponent", helpDocLinkPrefix: "status-api")
                .ReadContentAsAsync<ServiceStatus>();
        }

        private async Task<HttpResponseMessage> ExecuteRequest(
            Func<HttpClient, CancellationToken, Task<HttpResponseMessage>> requestBodyAccessor,
            string scope,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            using (_loggerScope(_logger, scope))
            using (var client = _httpClientFactory.CreateServiceStatusClient())
            {
                return await requestBodyAccessor(client, cancellationToken);
            }
        }
    }
}

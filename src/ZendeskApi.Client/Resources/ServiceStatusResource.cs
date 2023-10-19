using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models.Status.Response;
using ZendeskApi.Client.Resources.Interfaces;

namespace ZendeskApi.Client.Resources
{
    public class ServiceStatusResource : IServiceStatusResource
    {
        private readonly IZendeskApiClient _httpClientFactory;
        private readonly ILogger _logger;

        private readonly Func<ILogger, string, IDisposable> _loggerScope = LoggerMessage.DefineScope<string>(nameof(ServiceStatusResource) + ": {0}");

        public ServiceStatusResource(
            IZendeskApiClient httpClientFactory,
            ILogger logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<ListIncidentsResponse> ListActiveIncidents(
            string subdomain = null, 
            CancellationToken cancellationToken = default)
        {
            var requestUri = string.IsNullOrWhiteSpace(subdomain)
                ? "/api/incidents/active"
                : $"/api/incidents/active?subdomain={subdomain}";

            return await ExecuteRequest(async (client, token) =>
                        await client.GetAsync(requestUri, token).ConfigureAwait(false),
                    "ListActiveIncidents",
                    cancellationToken)
                .ThrowIfUnsuccessful("status_api#list-active-incidents", helpDocLinkPrefix: "status-api")
                .ReadContentAsAsync<ListIncidentsResponse>();
        }

        public async Task<IncidentResponse> GetActiveIncident(
            string incidentId, 
            CancellationToken cancellationToken = default)
        {
            var requestUri = $"/api/incidents/{incidentId}";

            return await ExecuteRequest(async (client, token) =>
                        await client.GetAsync(requestUri, token).ConfigureAwait(false),
                    "GetActiveIncident",
                    cancellationToken)
                .ThrowIfUnsuccessful("status_api#show-active-incident", helpDocLinkPrefix: "status-api")
                .ReadContentAsAsync<IncidentResponse>();
        }

        public async Task<ListMaintenanceIncidentsResponse> ListMaintenanceIncidents(
            string subdomain = null, 
            CancellationToken cancellationToken = default)
        {
            var requestUri = string.IsNullOrWhiteSpace(subdomain)
                ? "/api/incidents/maintenance"
                : $"/api/incidents/maintenance?subdomain={subdomain}";

            return await ExecuteRequest(async (client, token) =>
                        await client.GetAsync(requestUri, token).ConfigureAwait(false),
                    "ListMaintenanceIncidents",
                    cancellationToken)
                .ThrowIfUnsuccessful("status_api#list-maintenance-incidents", helpDocLinkPrefix: "status-api")
                .ReadContentAsAsync<ListMaintenanceIncidentsResponse>();
        }

        private async Task<HttpResponseMessage> ExecuteRequest(
            Func<HttpClient, CancellationToken, Task<HttpResponseMessage>> requestBodyAccessor,
            string scope,
            CancellationToken cancellationToken = default)
        {
            using (_loggerScope(_logger, scope))
            using (var client = _httpClientFactory.CreateServiceStatusClient())
            {
                return await requestBodyAccessor(client, cancellationToken);
            }
        }
    }
}

using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class JobStatusResource : IJobStatusResource
    {
        private const string ResourceUri = "api/v2/job_statuses";
        
        private readonly IZendeskApiClient _apiClient;
        private readonly ILogger _logger;

        private readonly Func<ILogger, string, IDisposable> _loggerScope = LoggerMessage.DefineScope<string>(typeof(JobStatusResource).Name + ": {0}");

        public JobStatusResource(IZendeskApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IPagination<JobStatusResponse>> ListAsync(PagerParameters pagerParameters = null)
        {
            using (_loggerScope(_logger, "ListAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(ResourceUri, pagerParameters).ConfigureAwait(false);

                await response.ThrowIfUnsuccessful("job_statuses#list-job-statuses");

                return await response.Content.ReadAsAsync<JobStatusListResponse>();
            }
        }

        public async Task<JobStatusResponse> GetAsync(string statusId)
        {
            using (_loggerScope(_logger, $"GetAsync({statusId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync(statusId).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.LogInformation($"JobStatus not found: {statusId}");
                    return null;
                }

                await response.ThrowIfUnsuccessful("job_statuses#show-job-status");

                var result = await response.Content.ReadAsAsync<SingleJobStatusResponse>();
                return result.JobStatus;
            }
        }

        public async Task<IPagination<JobStatusResponse>> GetAsync(string[] statusIds, PagerParameters pagerParameters = null)
        {
            using (_loggerScope(_logger, $"GetAsync({ZendeskFormatter.ToCsv(statusIds)})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync($"show_many?ids={ZendeskFormatter.ToCsv(statusIds)}", pagerParameters)
                    .ConfigureAwait(false);

                await response.ThrowIfUnsuccessful("job_statuses#show-many-job-statuses");

                return await response.Content.ReadAsAsync<JobStatusListResponse>();
            }
        }
    }
}
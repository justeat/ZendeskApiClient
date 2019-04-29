using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class JobStatusResource : AbstractBaseResource<JobStatusResource>,
        IJobStatusResource
    {
        private const string ResourceUri = "api/v2/job_statuses";
        
        public JobStatusResource(
            IZendeskApiClient apiClient, 
            ILogger logger)
            : base(apiClient, logger, "job_statuses")
        { }

        public async Task<IPagination<JobStatusResponse>> ListAsync(PagerParameters pagerParameters = null)
        {
            return await GetAsync<JobStatusListResponse>(
                ResourceUri,
                "list-job-statuses",
                "ListAsync",
                pagerParameters);
        }

        public async Task<JobStatusResponse> GetAsync(string statusId)
        {
            var response = await GetWithNotFoundCheckAsync<SingleJobStatusResponse>(
                $"{ResourceUri}/{statusId}",
                "show-job-status",
                $"GetAsync({statusId})",
                $"JobStatus {statusId} not found");

            return response?
                .JobStatus;
        }

        public async Task<IPagination<JobStatusResponse>> GetAsync(string[] statusIds, PagerParameters pagerParameters = null)
        {
            return await GetAsync<JobStatusListResponse>(
                $"{ResourceUri}/show_many?ids={ZendeskFormatter.ToCsv(statusIds)}",
                "show-many-job-statuses",
                $"GetAllAsync({ZendeskFormatter.ToCsv(statusIds)})",
                pagerParameters);
        }
    }
}
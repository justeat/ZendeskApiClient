using System.Collections.Generic;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class JobStatusResource : ZendeskResource<JobStatus>, IJobStatusResource
    {

        protected override string ResourceUri
        {
            get { return "/api/v2/job_statuses"; }
        }

        public JobStatusResource(IZendeskClient client)
        {
            Client = client;
        }

        public IResponse<JobStatus> Get(string jobId)
        {
            return Get<JobStatusResponse>(jobId);
        }

        public IListResponse<JobStatus> Get(List<string> jobIds)
        {
            return GetAll<JobStatusListResponse>(jobIds);
        }
    }
}

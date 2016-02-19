using System.Collections.Generic;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IJobStatusResource
    {
        IResponse<JobStatus> Get(string jobId);
        IListResponse<JobStatus> Get(List<string> jobIds);
    }
}

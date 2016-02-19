using System.Collections.Generic;
using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    internal class JobStatusListResponse : ListResponse<JobStatus>
    {
        [DataMember(Name = "job_statuses")]
        public override IEnumerable<JobStatus> Results { get; set; }
    }
}

using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class JobStatusResponse : IResponse<JobStatus>
    {
        [DataMember(Name = "job_status")]
        public JobStatus Item { get; set; }
    }
}

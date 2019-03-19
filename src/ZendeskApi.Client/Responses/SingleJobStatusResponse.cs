using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class SingleJobStatusResponse
    {
        [JsonProperty("job_status")]
        public JobStatusResponse JobStatus { get; set; }
    }
}
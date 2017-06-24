using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class JobStatusResponse
    {
        [JsonProperty("job_status")]
        public JobStatus Item { get; set; }
    }
}

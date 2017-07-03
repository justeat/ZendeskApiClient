using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    [JsonObject("job_status")]
    public class JobStatusResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("results")]
        public IEnumerable<JobStatusResult> Items { get; set; }
    }

    [Description("JobStatus")]
    public class JobStatusResult
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("errors")]
        public string Errors { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}

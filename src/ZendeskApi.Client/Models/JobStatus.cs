using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using ZendeskApi.Client.Converters;

namespace ZendeskApi.Client.Models
{
    [JsonObject("job_status")]
    public class JobStatusResponse
    {
        /// <summary>
        /// Automatically assigned when the job is queued
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The URL to poll for status updates
        /// </summary>
        [JsonProperty("url")]
        public Uri Url { get; set; }

        /// <summary>
        /// The total number of tasks this job is batching through
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// Number of tasks that have already been completed
        /// </summary>
        [JsonProperty("progress")]
        public int Progress { get; set; }

        /// <summary>
        /// The current status. One of the following: "queued", "working", "failed", "completed", "killed"
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Message from the job worker, if any
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Result data from processed tasks
        /// </summary>
        [JsonProperty("results")]
        [JsonConverter(typeof(JobStatusResultConverter))]
        public IEnumerable<JobStatusResult> Results { get; set; }
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

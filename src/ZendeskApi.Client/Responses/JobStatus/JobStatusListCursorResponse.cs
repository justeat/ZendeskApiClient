using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class JobStatusListCursorResponse : CursorPaginationResponse<JobStatusResponse>
    {
        [JsonProperty("job_statuses")]
        public IEnumerable<JobStatusResponse> JobStatuses { get; set; }

        protected override IEnumerable<JobStatusResponse> Enumerable => JobStatuses;
    }
}
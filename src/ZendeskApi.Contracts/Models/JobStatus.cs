using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace ZendeskApi.Contracts.Models
{
    [Description("JobStatus")]
    public class JobStatus
    {
        [JsonProperty]
        public string Id { get; set; }
    }
}

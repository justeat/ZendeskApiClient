using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ZendeskApi.Contracts.Models
{
    [Description("JobStatus")]
    [DataContract]
    public class JobStatus : IZendeskEntity
    {
        [JsonIgnore]
        public long? Id { get; set; }

        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string JobId { get; set; }

        [DataMember(Name = "url", EmitDefaultValue = false)]
        public Uri Url { get; set; }

        [DataMember(Name = "total", EmitDefaultValue = false)]
        public int? Total { get; set; }

        [DataMember(Name = "progress", EmitDefaultValue = false)]
        public int? Progress { get; set; }

        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string Message { get; set; }

        [DataMember(Name = "results", EmitDefaultValue = false)]
        public IEnumerable<JobStatusResult> Results { get; set; }
    }

    [Description("JobStatusResult")]
    [DataContract]
    public class JobStatusResult : IZendeskEntity
    {
        [DataMember(EmitDefaultValue = false)]
        public long? Id { get; set; }

        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; set; }

        [DataMember(Name = "action", EmitDefaultValue = false)]
        public string Action { get; set; }

        [DataMember(Name = "errors", EmitDefaultValue = false)]
        public string Errors { get; set; }

        [DataMember(Name = "success", EmitDefaultValue = false)]
        public bool? Success { get; set; }

        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }
    }
}

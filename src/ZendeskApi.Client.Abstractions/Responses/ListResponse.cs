﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Responses
{
    public class ListResponse<T> : IListResponse<T>
    {
        [JsonProperty("results")]
        public virtual IEnumerable<T> Results { get; set; }

        [JsonProperty("count")]
        public int TotalCount { get; set; }

        [JsonIgnore]
        public object Facets { get; set; }

        [JsonProperty("next_page")]
        public Uri NextPage { get; set; }

        [JsonProperty("previous_page")]
        public Uri PreviousPage { get; set; }
    }
}
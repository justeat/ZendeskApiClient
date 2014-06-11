﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class ListResponse<T> : IListResponse<T> where T : IZendeskEntity
    {
        [DataMember(Name = "results")]
        public virtual IEnumerable<T> Results { get; set; }

        [DataMember(Name = "count")]
        public int TotalCount { get; set; }

        [IgnoreDataMember]
        public object Facets { get; set; }

        [DataMember(Name = "next_page")]
        public Uri NextPage { get; set; }

        [DataMember(Name = "previous_page")]
        public Uri PreviousPage { get; set; }
    }
}

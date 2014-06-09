using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class TicketResponse
    {
        [DataMember(Name = "results")]
        public IEnumerable<Ticket> Tickets { get; set; }

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

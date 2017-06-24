using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Responses
{
    public interface IPagination<T> : IEnumerable<T>
    {
        IEnumerable<T> Item { get; set; }
        int Count { get; set; }
        Uri NextPage { get; set; }
        Uri PreviousPage { get; set; }
    }

    public abstract class PaginationResponse<T> : IPagination<T>
    {
        [JsonIgnore]
        public abstract IEnumerable<T> Item { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; } = 0;

        [JsonProperty("next_page")]
        public Uri NextPage { get; set; } = null;

        [JsonProperty("previous_page")]
        public Uri PreviousPage { get; set; } = null;

        public IEnumerator<T> GetEnumerator()
        {
            return Item.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Item.GetEnumerator();
        }
    }
}

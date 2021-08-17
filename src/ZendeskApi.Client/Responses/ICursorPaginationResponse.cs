using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Responses
{
    public interface ICursorPaginationResponse<T> : IEnumerable<T>
    {
    }

    public abstract class CursorPaginationResponse<T> : ICursorPaginationResponse<T>
    {

            [JsonProperty("has_more")]
            public bool HasMore { get; set; }
            [JsonProperty("after_cursor")]
            public string AfterCursor { get; set; }
            [JsonProperty("before_cursor")]
            public string BeforeCursor { get; set; }
        

      
            [JsonProperty("first")]
            public string First { get; set; }
            [JsonProperty("last")]
            public string Last { get; set; }
            [JsonProperty("next")]
            public string Next { get; set; }


        protected abstract IEnumerable<T> Enumerable { get; }

        public IEnumerator<T> GetEnumerator()
        {
            return Enumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Enumerable.GetEnumerator();
        }
    }
}
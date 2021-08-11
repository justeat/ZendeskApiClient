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
        public class Meta
        {
            [JsonProperty("has_more")]
            public bool HasMore { get; set; }
            [JsonProperty("after_cursor")]
            public string AfterCursor { get; set; }
            [JsonProperty("before_cursor")]
            public string BeforeCursor { get; set; }
        }

        public class Links
        {
            [JsonProperty("next")]
            public string Next { get; set; }
            [JsonProperty("prev")]
            public string Prev { get; set; }
        }

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
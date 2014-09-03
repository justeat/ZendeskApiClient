using System.Collections;
using System.Collections.Generic;

namespace ZendeskApi.Client.Configuration
{
    public class Headers : IEnumerable<KeyValuePair<string, string>>
    {
        private readonly IDictionary<string, string> _headers;

        public Headers()
        {
            _headers = new Dictionary<string, string>();
        }

        public void AddHeader(string key, string value)
        {
            RemoveHeader(key);
            if (string.IsNullOrWhiteSpace(value))
                return;
            _headers.Add(key, value);
        }

        private void RemoveHeader(string key)
        {
            if (!_headers.ContainsKey(key))
                return;
            _headers.Remove(key);
        }

        public string GetHeader(string key)
        {
            return _headers.ContainsKey(key) ? _headers[key] : null;
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _headers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

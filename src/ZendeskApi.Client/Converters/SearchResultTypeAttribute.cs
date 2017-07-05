using System;

namespace ZendeskApi.Client.Converters
{
    internal class SearchResultTypeAttribute : Attribute {
        public string ResultType { get; }

        public SearchResultTypeAttribute(string resultType)
        {
            ResultType = resultType;
        }
    }
}
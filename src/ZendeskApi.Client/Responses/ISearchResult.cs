using System;

namespace ZendeskApi.Client.Responses
{
    public interface ISearchResult
    {
        DateTime CreatedAt { get; }
        DateTime UpdatedAt { get; }
        long Id { get; }
        Uri Url { get; }
    }
}

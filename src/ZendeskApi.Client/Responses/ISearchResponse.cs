using System;

namespace ZendeskApi.Client.Responses
{
    public interface ISearchResponse
    {
        DateTime CreatedAt { get; }
        DateTime UpdatedAt { get; }
        long Id { get; }
        Uri Url { get; }
        string Type { get; }
    }
}

using System;

namespace ZendeskApi.Client.Models
{
    public interface ISearchResult
    {
        DateTime CreatedAt { get; }
        DateTime UpdatedAt { get; }
        long Id { get; }
        Uri Url { get; }
        string Type { get; }
    }
}

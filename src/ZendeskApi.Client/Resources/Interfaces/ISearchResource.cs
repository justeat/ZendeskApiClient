using System;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Queries;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ISearchResource
    {
        Task<SearchResponse<ISearchResult>> SearchAsync(
            Action<IZendeskQuery> builder, 
            PagerParameters pager = null, 
            CancellationToken cancellationToken = default);

        Task<SearchResponse<T>> SearchAsync<T>(
            Action<IZendeskQuery> builder, 
            PagerParameters pager = null, 
            CancellationToken cancellationToken = default)
            where T : ISearchResult;
    }
}
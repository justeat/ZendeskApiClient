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
        [Obsolete("Use `SearchAsync` with CursorPager parameter instead.")]
        Task<SearchResponse<ISearchResult>> SearchAsync(
            Action<IZendeskQuery> builder, 
            PagerParameters pager = null, 
            CancellationToken cancellationToken = default);

        Task<SearchCursorResponse<ISearchResult>> SearchAsync(
            Action<IZendeskQuery> builder,
            CursorPager pager,
            CancellationToken cancellationToken = default);


        [Obsolete("Use `SearchAsync` with CursorPager parameter instead.")]
        Task<SearchResponse<T>> SearchAsync<T>(
            Action<IZendeskQuery> builder, 
            PagerParameters pager = null, 
            CancellationToken cancellationToken = default)
            where T : ISearchResult;

        Task<SearchCursorResponse<T>> SearchAsync<T>(
            Action<IZendeskQuery> builder,
            CursorPager pager,
            CancellationToken cancellationToken = default)
            where T : ISearchResult;
    }
}